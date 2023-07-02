using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Module_8
{
    internal class Button
    {
        Texture2D butTex;
        Rectangle butRect;
        Color butColor;

        public Button(Texture2D butTex, Rectangle butRect, Color butColor)
        {
            this.butTex = butTex;
            this.butRect = butRect;
            this.butColor = butColor;
        }

        public Texture2D ButTex { set { butTex = value; } get { return butTex; }  }

        public Rectangle ButRect { set { butRect = value; } get {  return butRect; } }

        public Color ButColor { set { butColor = value; } get {  return butColor; } }
    }
}
