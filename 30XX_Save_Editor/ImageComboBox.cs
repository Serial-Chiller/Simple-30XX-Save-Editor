using Simple_30XX_Save_Editor;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _30XX_Save_Editor
{
    public class ImageComboBox : System.Windows.Forms.ComboBox
    {
        public ImageComboBox()
        {
            this.DrawMode = DrawMode.OwnerDrawFixed;
        }

        protected override void OnDrawItem(DrawItemEventArgs e)
        {
            e.DrawBackground();
            e.DrawFocusRectangle();

            if (e.Index >= 0)
            {
                ImageComboBoxItem item = (ImageComboBoxItem)Items[e.Index];
                e.Graphics.DrawImage(item.Image, e.Bounds.Left, e.Bounds.Top);
                e.Graphics.DrawString(item.Text, e.Font, new SolidBrush(e.ForeColor), e.Bounds.Left + item.Image.Width, e.Bounds.Top);
            }
            base.OnDrawItem(e);
        }
    }
    public class ImageComboBoxItem
    {
        public string Text { get; set; }
        public Image Image { get; set; }

        public ImageComboBoxItem(string text, int key, Image image)
        {
            this.Text = text;
            this.Image = image;
        }

        public override string ToString()
        {
            return Text;
        }
    }
}
