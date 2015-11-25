﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace AxisEngine.Visuals
{
    public class Animation
    {
        private Texture2D _atlas;
        private Rectangle _sourceRectangle;
        private int _rows;
        private int _columns;
        private int _current;
        private int _cellWidth;
        private int _cellHeight;
        private int _totalCells;
        private float _frameTime;
        private float _timer;

        /// <summary>
        /// totalCells is the number of occupied cells in "reading" order (left -> right, top -> bottom)
        /// if totalCells is 0 or less, it is assumed that all cells are used.
        /// Duration is in milliseconds
        /// </summary>
        public Animation(Texture2D atlas, float duration, int rows, int columns, int totalCells = 0)
        {
            _atlas = atlas;
            _rows = rows;
            _columns = columns;
            _current = 0;
            _totalCells = totalCells > 0 ?
                totalCells :
                _rows * _columns;
            _frameTime = duration / _totalCells;
            _timer = 0;
            _cellWidth = atlas.Width / _columns;
            _cellHeight = atlas.Height / _rows;

            _sourceRectangle = new Rectangle(0, 0, _cellWidth, _cellHeight);
            ResetSourceRectangle();
        }

        public void Update(GameTime t)
        {
            _timer += t.ElapsedGameTime.Milliseconds;
            if(_timer > _frameTime)
            {
                _timer %= _frameTime;
                _current = (_current + 1) % _totalCells;
                ResetSourceRectangle();
            }
        }

        public Texture2D Texture
        {
            get { return _atlas; }
        }

        public Rectangle SourceRectangle
        {
            get { return _sourceRectangle; }
        }

        public float Duration
        {
            get { return _frameTime * _totalCells; }
            set { _frameTime = value / _totalCells; }
        }

        public void Reset()
        {
            _timer = 0;
            _current = 0;
            ResetSourceRectangle();
        }
        
        private void ResetSourceRectangle()
        {
            int row = _current / _columns;
            int col = _current % _columns;
            _sourceRectangle.X = col * _cellWidth;
            _sourceRectangle.Y = row * _cellHeight;
        }
    }
}