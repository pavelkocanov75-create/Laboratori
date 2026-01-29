/* FlexibleGridLayout.cs
 * From: Game Dev Guide - Fixing Grid Layouts in Unity With a Flexible Grid Component
 * Created: June 2020, NowWeWake
 */

using System;
using UnityEngine;
using UnityEngine.UI;

namespace ARLaboratory.Card
{
    public class GridLayout : LayoutGroup
    {
        [Header("Flexible Grid")]
        [SerializeField] private FitType _fitType = FitType.UNIFORM;
        [SerializeField] private int _rows;
        [SerializeField] private int _columns;
        [SerializeField] private Vector2 _cellSize;
        [SerializeField] private Vector2 _spacing;
        [SerializeField] private bool _fitX;
        [SerializeField] private bool _fitY;

        private enum FitType
        {
            UNIFORM,
            WIDTH,
            HEIGHT,
            FIXEDROWS,
            FIXEDCOLUMNS
        }
    
        public override void CalculateLayoutInputHorizontal()
        {
            base.CalculateLayoutInputHorizontal();
        
            SetFitParameters();
        
            SetRows();
        
            SetColumns();
        
            CalculateCellsSize();

            CreateGridLayout();
        }

        private void CalculateCellsSize()
        {
            var rect = rectTransform.rect;
            float parentWidth = rect.width;
            float parentHeight = rect.height;

            float cellWidth = (parentWidth / _columns) - (_spacing.x / ((float)_columns / (_columns - 1))) -
                              (padding.left / (float)_columns) - (padding.right / (float)_columns);
            float cellHeight = (parentHeight / _rows) - (_spacing.y / ((float)_rows / (_rows - 1))) -
                               (padding.top / (float)_rows) - (padding.bottom / (float)_rows);

            _cellSize.x = _fitX ? cellWidth : _cellSize.x;
            _cellSize.y = _fitY ? cellHeight : _cellSize.y;
        }

        private void SetColumns()
        {
            if (_fitType is FitType.HEIGHT or FitType.FIXEDROWS)
            {
                _columns = Mathf.CeilToInt(transform.childCount / (float)_rows);
            }
        }

        private void SetRows()
        {
            if (_fitType is FitType.WIDTH or FitType.FIXEDCOLUMNS)
            {
                _rows = Mathf.CeilToInt(transform.childCount / (float)_columns);
            }
        }

        private void SetFitParameters()
        {
            if (_fitType is FitType.WIDTH or FitType.HEIGHT or FitType.UNIFORM)
            {
                float squareRoot = Mathf.Sqrt(transform.childCount);
                _rows = _columns = Mathf.CeilToInt(squareRoot);
                switch (_fitType)
                {
                    case FitType.WIDTH:
                        _fitX = true;
                        _fitY = false;
                        break;
                    case FitType.HEIGHT:
                        _fitX = false;
                        _fitY = true;
                        break;
                    case FitType.UNIFORM:
                        _fitX = _fitY = true;
                        break;
                    case FitType.FIXEDROWS:
                        break;
                    case FitType.FIXEDCOLUMNS:
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }

        private void CreateGridLayout()
        {
            for (int i = 0; i < rectChildren.Count; i++)
                CreateGridCell(i);
        }

        private void CreateGridCell(int i)
        {
            int rowCount = i / _columns;
            int columnCount = i % _columns;

            RectTransform item = rectChildren[i];

            float xPos = (_cellSize.x * columnCount) + (_spacing.x * columnCount) + padding.left;
            float yPos = (_cellSize.y * rowCount) + (_spacing.y * rowCount) + padding.top;

            SetChildAlongAxis(item, 0, xPos, _cellSize.x);
            SetChildAlongAxis(item, 1, yPos, _cellSize.y);
        }

        public override void CalculateLayoutInputVertical()
        {
        
        }

        public override void SetLayoutHorizontal()
        {

        }

        public override void SetLayoutVertical()
        {

        }
    }
}