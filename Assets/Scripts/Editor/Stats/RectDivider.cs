using UnityEngine;

namespace Editor.Stats
{
    public class RectDivider
    {
        private int _lineNeeded;
        private Rect _fullPos;
        private Rect _currLine;

        private float _lineHeight;
        private float _currentHeight;

        public RectDivider(Rect position, int lineNeeded)
        {
            _fullPos = position;
            _lineNeeded = lineNeeded;

            _lineHeight = _fullPos.height / lineNeeded;

            _currentHeight = _fullPos.y;
        }

        public Rect GetNext(int numberOfLine = 1)
        {
            var currLineHeight = _lineHeight * numberOfLine;
            
            var rect = new Rect(_fullPos.x, _currentHeight, _fullPos.width, currLineHeight);

            _currentHeight += currLineHeight;

            return rect;
        }
    }
}