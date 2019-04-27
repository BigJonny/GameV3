using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GameProject_V3.Controls
{
    /// <summary>
    /// Eine Klasse, die die einfachsten Funktionen einer Textbox implementieren.
    /// </summary>
    public class TextBox : GameControl
    {

        #region Varaiblen:
        private string text;
        private MouseState state;
        private bool canWrite = true;
        private bool isWriting = false;
        private int textIndex = 0;
        private Bitmap textIndexImage;
        private int offsetX = 0;

        private static List<Tuple<Keys, char>> translation;
        private static List<Tuple<Keys, char>> translationUpper;

        private static object textChangedKey = new object();
        private static object textIndexChangedKey = new object();
        #endregion

        /// <summary>
        /// Erstellt eine neue leere TextBox.
        /// </summary>
        public TextBox()
        {
            text = "";
            CanFocus = true;
            InitImages();
            InitTranslation();
        }

        #region Overrides:
        protected override void DrawControl(Graphics graphics, GameTime gameTime)
        {
            base.DrawControl(graphics, gameTime);
            Font.DrawString(text, this.graphics, new Rectangle(0 - offsetX, 0, Width, Height), TextAlignment.Left);
            if(HasFocus)
            {
                if (gameTime.Milliseconds % 100 <= 50)
                {
                    Point textSize = Font.MeasureString(text.Substring(0, textIndex));
                    this.graphics.DrawImage(textIndexImage, new Point(textSize.X - offsetX, 0));
                }
            }
            graphics.DrawImage(DrawArea, Bounds);
        }

        protected override void UpdateControl(GameTime gameTime)
        {
            base.UpdateControl(gameTime);
            state = MouseInput.GetState();
            if (isWriting == true)
            {
                if (gameTime.Milliseconds % 25 == 0)
                {
                    canWrite = true;
                }
                else
                    canWrite = false;
            }
            else
                canWrite = true;
            if(IsPointInside(state.Position) == false)
            {
                if(state.LeftButton == ButtonStates.Pressed)
                {
                    HasFocus = false;
                }
            }
            isWriting = CheckWriteing(KeyBoardInput.GetState());
        }

        protected override void OnClick(object sender, MouseEventArgs args)
        {
            base.OnClick(sender, args);
            HasFocus = true;
        }

        protected override void OnSizeChanged(object sender, EventArgs args)
        {
            base.OnSizeChanged(sender, args);
            InitImages();
        }

        protected override void OnKeyDown(object sender, KeyEventArgs args)
        {
            base.OnKeyDown(sender, args);
            if(canWrite)
            {
                if (args.CurrentState.IsAnyKeyDown)
                {
                    Keys[] ks = args.CurrentState.GetPressedKeys();
                    foreach (Keys k in ks)
                    {
                        if (k == Keys.Space)
                        {
                            text = text.Insert(textIndex, " ");
                            IncrementIndex();
                            textIndex++;
                            OnTextIndexChanged(this, EventArgs.Empty);
                            OnTextChanged(this, EventArgs.Empty);
                            isWriting = true;
                            break;
                        }
                        else if(k == Keys.Back)
                        {
                            if(textIndex > 0)
                            {
                                DecrementIndex();
                                textIndex--;
                                text = text.Remove(textIndex, 1);
                                OnTextIndexChanged(this, EventArgs.Empty);
                                OnTextChanged(this, EventArgs.Empty);
                                isWriting = true;
                                break;
                            }
                        }
                        else if(k == Keys.Left)
                        {
                            if (textIndex > 0)
                            {
                                DecrementIndex();
                                textIndex--;
                                OnTextIndexChanged(this, EventArgs.Empty);
                                isWriting = true;
                                break;
                            }
                        }
                        else if(k == Keys.Right)
                        {
                            if(textIndex <= text.Length - 1)
                            {
                                IncrementIndex();
                                textIndex++;
                                OnTextIndexChanged(this, EventArgs.Empty);
                                isWriting = true;
                                break;
                            }
                        }
                        else if (k.ToString().Length == 1)
                        {//Buchstabe gedrückt:
                            if (ks.Contains(Keys.ShiftKey) == true)
                            {
                                text = text.Insert(textIndex, k.ToString());
                                IncrementIndex();
                                textIndex++;
                                OnTextIndexChanged(this, EventArgs.Empty);
                                OnTextChanged(this, EventArgs.Empty);
                                isWriting = true;
                                break;
                            }
                            else
                            {
                                text = text.Insert(textIndex, k.ToString().ToLower());
                                IncrementIndex();
                                textIndex++;
                                OnTextIndexChanged(this, EventArgs.Empty);
                                OnTextChanged(this, EventArgs.Empty);
                                isWriting = true;
                                break;
                            }
                        }
                        else
                        {
                            if (ContainsTranslation(k) != null)
                            {//Zahl oder Zeichen gefunden:
                                if (ks.Contains(Keys.ShiftKey) == false)
                                {
                                    text = text.Insert(textIndex, ContainsTranslation(k).Item2 + "");
                                    IncrementIndex();
                                    textIndex++;
                                    OnTextIndexChanged(this, EventArgs.Empty);
                                    OnTextChanged(this, EventArgs.Empty);
                                    isWriting = true;
                                    break;
                                }
                                else
                                {
                                    text = text.Insert(textIndex, GetTranslationUpper(k).Item2 + "");
                                    IncrementIndex();
                                    textIndex++;
                                    OnTextIndexChanged(this, EventArgs.Empty);
                                    OnTextChanged(this, EventArgs.Empty);
                                    isWriting = true;
                                    break;
                                }
                            }
                        }
                    }
                }
            }
        }
        #endregion

        #region Hilfsfunktionen:
        /// <summary>
        /// Initialsiert alle nötigen Bilder.
        /// </summary>
        private void InitImages()
        {
            textIndexImage = new Bitmap(5, Height);
            Graphics g = Graphics.FromImage(textIndexImage);
            g.Clear(Color.Black);
        }

        /// <summary>
        /// Initialisiert die Übersetzungen von Keys zu chars.
        /// </summary>
        private void InitTranslation()
        {
            translation = new List<Tuple<Keys, char>>();
            translationUpper = new List<Tuple<Keys, char>>();
            translation.Add(new Tuple<Keys, char>(Keys.D1, '1'));
            translation.Add(new Tuple<Keys, char>(Keys.D2, '2'));
            translation.Add(new Tuple<Keys, char>(Keys.D3, '3'));
            translation.Add(new Tuple<Keys, char>(Keys.D4, '4'));
            translation.Add(new Tuple<Keys, char>(Keys.D5, '5'));
            translation.Add(new Tuple<Keys, char>(Keys.D6, '6'));
            translation.Add(new Tuple<Keys, char>(Keys.D7, '7'));
            translation.Add(new Tuple<Keys, char>(Keys.D8, '8'));
            translation.Add(new Tuple<Keys, char>(Keys.D9, '9'));
            translation.Add(new Tuple<Keys, char>(Keys.D0, '0'));
            translation.Add(new Tuple<Keys, char>(Keys.Oemplus, '+'));
            translation.Add(new Tuple<Keys, char>(Keys.OemQuestion, '#'));
            translation.Add(new Tuple<Keys, char>(Keys.OemPeriod, '.'));
            translation.Add(new Tuple<Keys, char>(Keys.OemMinus, '-'));
            translation.Add(new Tuple<Keys, char>(Keys.Oem1, 'ü'));
            translation.Add(new Tuple<Keys, char>(Keys.Oem6, '´'));
            translation.Add(new Tuple<Keys, char>(Keys.Oem7, 'ä'));
            translation.Add(new Tuple<Keys, char>(Keys.Oemtilde, 'ö'));
            translation.Add(new Tuple<Keys, char>(Keys.Oemcomma, ','));

            translationUpper.Add(new Tuple<Keys, char>(Keys.D1, '!'));
            translationUpper.Add(new Tuple<Keys, char>(Keys.D2, '"'));
            translationUpper.Add(new Tuple<Keys, char>(Keys.D3, '§'));
            translationUpper.Add(new Tuple<Keys, char>(Keys.D4, '$'));
            translationUpper.Add(new Tuple<Keys, char>(Keys.D5, '%'));
            translationUpper.Add(new Tuple<Keys, char>(Keys.D6, '&'));
            translationUpper.Add(new Tuple<Keys, char>(Keys.D7, '/'));
            translationUpper.Add(new Tuple<Keys, char>(Keys.D8, '('));
            translationUpper.Add(new Tuple<Keys, char>(Keys.D9, ')'));
            translationUpper.Add(new Tuple<Keys, char>(Keys.D0, '?'));
            translationUpper.Add(new Tuple<Keys, char>(Keys.Oemplus, '*'));
            translationUpper.Add(new Tuple<Keys, char>(Keys.OemQuestion, '#'));
            translationUpper.Add(new Tuple<Keys, char>(Keys.OemPeriod, ':'));
            translationUpper.Add(new Tuple<Keys, char>(Keys.OemMinus, '_'));
            translationUpper.Add(new Tuple<Keys, char>(Keys.Oem1, 'Ü'));
            translationUpper.Add(new Tuple<Keys, char>(Keys.Oem6, '`'));
            translationUpper.Add(new Tuple<Keys, char>(Keys.Oem7, 'Ä'));
            translationUpper.Add(new Tuple<Keys, char>(Keys.Oemtilde, 'Ö'));
            translationUpper.Add(new Tuple<Keys, char>(Keys.Oemcomma, ';'));
        }

        /// <summary>
        /// Schaut, ob sich die zu dem übergeben <see cref="Keys"/>-Enumartion die passende übersetzung gibt.
        /// </summary>
        /// <param name="k"></param>
        /// <returns></returns>
        private Tuple<Keys, char> ContainsTranslation(Keys k)
        {
            foreach(Tuple<Keys, char> tuple in translation)
            {
                if (tuple.Item1 == k)
                {
                    return tuple;
                }
            }
            return null;
        }

        /// <summary>
        /// Schaut, ob sich die zu dem übergeben <see cref="Keys"/>-Enumartion die passende Übersetzung gibt.
        /// </summary>
        /// <param name="k"></param>
        /// <returns></returns>
        private Tuple<Keys, char> GetTranslationUpper(Keys k)
        {
            foreach(Tuple<Keys, char> tuple in translationUpper)
            {
                if(k == tuple.Item1)
                {
                    return tuple;
                }
            }
            return null;
        }

        /// <summary>
        /// Prüft, ob zum Zeitpunkt des Aufrufs in das Steuerelement geschrieben wird.
        /// </summary>
        /// <param name="kState"></param>
        /// <returns></returns>
        private bool CheckWriteing(KeyboardState kState)
        {
            if(kState.IsAnyKeyDown == false)
            {
                return false;
            }
            else if(kState.GetPressedKeys().Contains(Keys.ShiftKey) && kState.GetPressedKeys().Count() == 1)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// Dekrementiert den <see cref="TextIndex"/>.
        /// </summary>
        private void DecrementIndex()
        {
            if(IsTextOutOfBounds(text))
            {
                if(IsTextOutOfBounds(text.Substring(0, textIndex)))
                {
                    offsetX -= Font.MeasureChar(text.ToCharArray()[textIndex - 1]).X;
                    if (offsetX < 0)
                    {
                        offsetX = 0;
                    }
                }
            }
        }

        /// <summary>
        /// Inkrementiert den <see cref="TextIndex"/>.
        /// </summary>
        private void IncrementIndex()
        {
            if(IsTextOutOfBounds(text))
            {
                if(IsTextOutOfBounds(text.Substring(0, textIndex)))
                {
                    offsetX += Font.MeasureChar(text.ToCharArray()[textIndex - 1]).X;
                }
            }
        }

        /// <summary>
        /// Gibt true zurück, wenn der Text größer ist als das Steuerlement.
        /// </summary>
        /// <returns></returns>
        private bool IsTextOutOfBounds(string text)
        {
            if(Font.MeasureString(text).X > Width - 20)
            {
                return true;
            }
            return false;
        }
        #endregion

        #region Events:
        /// <summary>
        /// Tritt ein, wenn sich die <see cref="Text"/>-Eigenschaft verändert hat.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        protected virtual void OnTextChanged(object sender, EventArgs args)
        {
            EventHandler handler;
            handler = (EventHandler)Events[textChangedKey];
            handler?.Invoke(sender, args);
        }

        /// <summary>
        /// Tritt ein, wenn sich der aktuelle Index zum Einfügen oder Löschen von Text geändert hat.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        protected virtual void OnTextIndexChanged(object sender, EventArgs args)
        {
            EventHandler handler;
            handler = (EventHandler)Events[textIndexChangedKey];
            handler?.Invoke(sender, args);
        }
        #endregion

        #region Eigenschaften:
        /// <summary>
        /// Gibt den Text den Steuerelements zurück oder überschreibt diesen.
        /// </summary>
        public string Text
        {
            get
            {
                return text;
            }
            set
            {
                text = value;
                textIndex = text.Length - 1;
                OnTextChanged(this, EventArgs.Empty);
                OnTextIndexChanged(this, EventArgs.Empty);
            }
        }

        /// <summary>
        /// Gibt den aktuellen Index zum einfügen oder Löschen von Text zurück.
        /// </summary>
        public int TextIndex
        {
            get
            {
                return textIndex;
            }
        }

        /// <summary>
        /// Ein Ereignis, welches eintritt, wenn sich der <see cref="Text"/> geändert hat.
        /// </summary>
        public event EventHandler TextChanged
        {
            add
            {
                Events.AddHandler(textChangedKey, value);
            }
            remove
            {
                Events.RemoveHandler(textChangedKey, value);
            }
        }

        /// <summary>
        /// Ein Ereignis, welches eintritt, wenn sich der Index zum Einfügen oder Löschen von Tet geändert hat.
        /// </summary>
        public event EventHandler TextIndexChanged
        {
            add
            {
                Events.AddHandler(textIndexChangedKey, value);
            }
            remove
            {
                Events.RemoveHandler(textIndexChangedKey, value);
            }
        }
        #endregion
    }
}
