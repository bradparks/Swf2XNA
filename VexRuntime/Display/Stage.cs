﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace DDW.Display
{
    public class Stage : DisplayObjectContainer
    {
        public float MillisecondsPerFrame = 1000f / 12f;

        protected Stage()
        {
            stage = this;
        }
        internal virtual void ObjectAddedToStage(DisplayObject o)
        {
        }
        internal virtual void ObjectRemovedFromStage(DisplayObject o)
        {
        }
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }
        public override void Draw(SpriteBatch batch)
        {
            batch.Begin();
            base.Draw(batch);
            batch.End();
        }
    }
}