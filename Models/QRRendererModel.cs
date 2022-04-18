using System;
using System.Collections.Generic;
using System.Drawing;

namespace QRCodeGenerator.Models
{
    class QRRendererModel
    {
        private int _version;
        private int _errorCorrectionLevel;
        private int _matrixSize;
        private Bitmap picture;

        public bool[,] QRMatrix;

        private int[][] alignmentSquaresCoords = new int[][] {
                new int[] {18}, new int[] {22}, new int[] {26}, new int[] {30}, new int[] {34}, new int[] { 6 , 22, 38},
                new int[] {6, 24, 42}, new int[] {6, 26, 46}, new int[] {6, 28, 50}, new int[] {6, 30, 54}, new int[] {6, 32, 58}, new int[] {6, 34, 62},
                new int[] {6, 26, 46, 66}, new int[] {6, 26, 48, 70}, new int[] {6, 26, 50, 74}, new int[] {6, 30, 54, 78}, new int[] {6, 30, 56, 82},
                new int[] {6, 30, 58, 86}, new int[] {6, 34, 62, 90}, new int[] {6, 28, 50, 72, 94}, new int[] {6, 26, 50, 74, 98}, new int[] {6, 30, 54, 78, 102},
                new int[] {6, 28, 54, 80, 106}, new int[] {6, 32, 58, 84, 110}, new int[] {6, 30, 58, 86, 114}, new int[] {6, 34, 62, 90, 118}, new int[] {6, 26, 50, 74, 98, 122},
                new int[] {6, 30, 54, 78, 102, 126}, new int[] {6, 26, 52, 78, 104, 130}, new int[] {6, 30, 56, 82, 108, 134}, new int[] {6, 34, 60, 86, 112, 138},
                new int[] {6, 30, 58, 86, 114, 142}, new int[] {6, 34, 62, 90, 118, 146}, new int[] {6, 30, 54, 78, 102, 126, 150}, new int[] {6, 24, 50, 76, 102, 128, 154},
                new int[] {6, 28, 54, 80, 106, 132, 158}, new int[] {6, 32, 58, 84, 110, 136, 162}, new int[] {6, 26, 54, 82, 110, 138, 166}, new int[] {6, 30, 58, 86, 114, 142, 170}
            };

        //только 0 маска
        private bool[][] maskCodes = new bool[][] {
        new bool[] { true, true, true, false, true, true, true, true, true, false, false, false, true, false, false },
        new bool[] { true, false, true, false, true, false, false, false, false, false, true, false, false, true, false },
        new bool[] { false, true, true, false, true, false, true, false, true, false, true, true, true, true, true },
        new bool[] { false, false, true, false, true, true, false, true, false, false, false, true, false, false, true }
        };

        public QRRendererModel(int ver, int errCorrection)
        {
            _version = ver;
            _errorCorrectionLevel = errCorrection;
            _matrixSize = (ver + 1) * 4 + 17 + 2;
            QRMatrix = new bool[_matrixSize, _matrixSize];
        }

        private void DrawInfo()
        {
            int i, j, x, y, pointx, pointy;
            bool pixelColour;
            bool[,] trackerSquare = {
                                       { false, false, false, false, false, false, false, false, false },
                                       { false, true,  true,  true,  true,  true,  true,  true,  false },
                                       { false, true,  false, false, false, false, false, true,  false },
                                       { false, true,  false, true,  true,  true,  false, true,  false },
                                       { false, true,  false, true,  true,  true,  false, true,  false },
                                       { false, true,  false, true,  true,  true,  false, true,  false },
                                       { false, true,  false, false, false, false, false, true,  false },
                                       { false, true,  true,  true,  true,  true,  true,  true,  false },
                                       { false, false, false, false, false, false, false, false, false } };

            bool[,] alignmentSquare = { { true, true, true, true, true, },
                                      { true, false, false, false, true, },
                                      { true, false, true, false, true, },
                                      { true, false, false, false, true, },
                                      { true, true, true, true, true, }};


            bool[][] versionInfo = new bool[][] {
                new bool[] { false, false, false, false, true, false, false, true, true, true, true, false, true, false, false, true, true, false, },
                new bool[] { false, true, false, false, false, true, false, true, true, true, false, false, true, true, true, false, false, false, },
                new bool[] { true, true, false, true, true, true, false, true, true, false, false, false, false, false, false, true, false, false, },
                new bool[] { true, false, true, false, false, true, true, true, true, true, true, false, false, false, false, false, false, false, },
                new bool[] { false, false, true, true, true, true, true, true, true, false, true, false, true, true, true, true, false, false, },
                new bool[] { false, false, true, true, false, true, true, false, false, true, false, false, false, true, true, false, true, false, },
                new bool[] { true, false, true, false, true, true, true, false, false, false, false, false, true, false, false, true, true, false, },
                new bool[] { true, true, false, true, false, true, false, false, false, true, true, false, true, false, false, false, true, false, },
                new bool[] { false, true, false, false, true, true, false, false, false, false, true, false, false, true, true, true, true, false, },
                new bool[] { false, true, true, true, false, false, false, true, false, false, false, true, false, true, true, true, false, false, },
                new bool[] { true, true, true, false, true, false, false, true, false, true, false, true, true, false, false, false, false, false, },
                new bool[] { true, false, false, true, false, false, true, true, false, false, true, true, true, false, false, true, false, false, },
                new bool[] { false, false, false, false, true, false, true, true, false, true, true, true, false, true, true, false, false, false, },
                new bool[] { false, false, false, false, false, false, true, false, true, false, false, true, true, true, true, true, true, false, },
                new bool[] { true, false, false, true, true, false, true, false, true, true, false, true, false, false, false, false, true, false, },
                new bool[] { true, true, true, false, false, false, false, false, true, false, true, true, false, false, false, true, true, false, },
                new bool[] { false, true, true, true, true, false, false, false, true, true, true, true, true, true, true, false, true, false, },
                new bool[] { false, false, true, true, false, true, false, false, true, true, false, true, true, false, false, true, false, false, },
                new bool[] { true, false, true, false, true, true, false, false, true, false, false, true, false, true, true, false, false, false, },
                new bool[] { true, true, false, true, false, true, true, false, true, true, true, true, false, true, true, true, false, false, },
                new bool[] { false, true, false, false, true, true, true, false, true, false, true, true, true, false, false, false, false, false, },
                new bool[] { false, true, false, false, false, true, true, true, false, true, false, true, false, false, false, true, true, false, },
                new bool[] { true, true, false, true, true, true, true, true, false, false, false, true, true, true, true, false, true, false, },
                new bool[] { true, false, true, false, false, true, false, true, false, true, true, true, true, true, true, true, true, false, },
                new bool[] { false, false, true, true, true, true, false, true, false, false, true, true, false, false, false, false, true, false, },
                new bool[] { true, false, true, false, false, false, false, true, true, false, false, false, true, false, true, true, false, true, },
                new bool[] { false, false, true, true, true, false, false, true, true, true, false, false, false, true, false, false, false, true, },
                new bool[] { false, true, false, false, false, false, true, true, true, false, true, false, false, true, false, true, false, true, },
                new bool[] { true, true, false, true, true, false, true, true, true, true, true, false, true, false, true, false, false, true, },
                new bool[] { true, true, false, true, false, false, true, false, false, false, false, false, false, false, true, true, true, true, },
                new bool[] { false, true, false, false, true, false, true, false, false, true, false, false, true, true, false, false, true, true, },
                new bool[] { false, false, true, true, false, false, false, false, false, false, true, false, true, true, false, true, true, true, },
                new bool[] { true, false, true, false, true, false, false, false, false, true, true, false, false, false, true, false, true, true, },
                new bool[] { true, true, true, false, false, true, false, false, false, true, false, false, false, true, false, true, false, true, },

            };

            //рисует квадраты трекера
            for (x = 0; x < 9; x++)
            {
                for (y = 0; y < 9; y++)
                {
                    //верхний левый квадрат
                    QRMatrix[x, y] = trackerSquare[x, y];

                    //поскольку при x=y происходит отражение, x и y можно поменять местами
                    //два других квадрата
                    QRMatrix[_matrixSize - x - 1, y] = trackerSquare[x, y]; ;
                    QRMatrix[y, _matrixSize - x - 1] = trackerSquare[x, y]; ;
                }
            }

            // временные линии
            for (x = 9; x < _matrixSize - 9; x++)
            {
                pixelColour = (x % 2 == 1);
                QRMatrix[x, 7] = pixelColour;
                QRMatrix[7, x] = pixelColour;
            }

            if (_version > 0)
            {
                for (i = 0; i < alignmentSquaresCoords[_version - 1].Length; i++)
                {
                    pointx = alignmentSquaresCoords[_version - 1][i] - 1;
                    for (j = 0; j < alignmentSquaresCoords[_version - 1].Length; j++)
                    {
                        pointy = alignmentSquaresCoords[_version - 1][j] - 1;
                        if ((pointy < 9 && (pointx < 9 || pointx + 5 > _matrixSize - 9)) || (pointy + 5 > _matrixSize - 9 && pointx < 9))
                        {
                            continue;
                        }
                        for (x = 0; x < 5; x++)
                        {
                            for (y = 0; y < 5; y++)
                            {
                                QRMatrix[pointy + y, pointx + x] = alignmentSquare[y, x];
                            }
                        }
                    }
                }
            }

            if (_version > 5)
            {
                Queue<bool> versionInfoQueue = new Queue<bool>(versionInfo[_version - 6]);
                for (i = 0; i < 18; i++)
                {
                    versionInfoQueue.Enqueue(versionInfo[_version - 6][i]);
                }
                for (y = 0; y < 3; y++)
                {
                    for (x = 1; x < 7; x++)
                    {
                        bool tempBool = versionInfoQueue.Dequeue();
                        QRMatrix[(_matrixSize - 12) + y, x] = tempBool;
                        QRMatrix[x, (_matrixSize - 12) + y] = tempBool;
                    }
                }
            }

            // всегда  черная точка
            QRMatrix[_matrixSize - 9, 9] = true;
        }

        private void DrawData(Queue<bool> data)
        {
            int row, col, colMod, i, j, pointx, pointy;
            bool direction = true;
            DrawInfo();

            for (col = _matrixSize - 2; col > 0; col -= 2)
            {
                if (col == 7)
                    col--;
                if (direction)
                {
                    for (row = _matrixSize - 2; row > 0; row--)
                    {
                        for (colMod = 0; colMod < 2; colMod++)
                        {
                            // исключение порчи служебной информации
                            if (((col - colMod) < 10 && (row < 10 || row > _matrixSize - 10)) || ((col - colMod) > _matrixSize - 10 && row < 10) || row == 7 || (col - colMod) == 7)
                            {
                                continue;
                            }
                            // обход выранивающих узоров
                            if (_version > 0)
                            {
                                for (i = 0; i < alignmentSquaresCoords[_version - 1].Length; i++)
                                {
                                    pointx = alignmentSquaresCoords[_version - 1][i] - 1;
                                    for (j = 0; j < alignmentSquaresCoords[_version - 1].Length; j++)
                                    {
                                        pointy = alignmentSquaresCoords[_version - 1][j] - 1;
                                        if ((alignmentSquaresCoords[_version - 1][i] >= _matrixSize - 9 && alignmentSquaresCoords[_version - 1][j] <= 6) || (alignmentSquaresCoords[_version - 1][j] >= _matrixSize - 9 && alignmentSquaresCoords[_version - 1][i] <= 6))
                                        {
                                            continue;
                                        }
                                        if (((col - colMod) >= pointx && (col - colMod) <= pointx + 4) && (row >= pointy && row <= pointy + 4))
                                        {
                                            break;
                                        }
                                    }
                                    if (j < alignmentSquaresCoords[_version - 1].Length)
                                        break;
                                }
                                if (i != alignmentSquaresCoords[_version - 1].Length)
                                    continue;
                            }
                            if (_version > 5 && (((col - colMod) > _matrixSize - 13 && (row < 7)) || (row > _matrixSize - 13 && ((col - colMod) < 7))))
                                continue;
                            if (data.Count != 0)
                                QRMatrix[row, col - colMod] = data.Dequeue();
                            if (((col - colMod) + row) % 2 == 0)
                            {
                                QRMatrix[row, col - colMod] = !QRMatrix[row, col - colMod];
                            }
                        }
                    }
                    direction = false;
                }
                else
                {
                    for (row = 1; row < _matrixSize - 1; row++)
                    {
                        for (colMod = 0; colMod < 2; colMod++)
                        {
                            if (((col - colMod) < 10 && (row < 10 || row > _matrixSize - 10)) || ((col - colMod) > _matrixSize - 10 && row < 10) || row == 7 || (col - colMod) == 7)
                            {
                                continue;
                            }
                            if (_version > 0)
                            {
                                for (i = 0; i < alignmentSquaresCoords[_version - 1].Length; i++)
                                {
                                    pointx = alignmentSquaresCoords[_version - 1][i] - 1;
                                    for (j = 0; j < alignmentSquaresCoords[_version - 1].Length; j++)
                                    {
                                        pointy = alignmentSquaresCoords[_version - 1][j] - 1;
                                        if ((alignmentSquaresCoords[_version - 1][i] >= _matrixSize - 9 && alignmentSquaresCoords[_version - 1][j] <= 6) || (alignmentSquaresCoords[_version - 1][j] >= _matrixSize - 9 && alignmentSquaresCoords[_version - 1][i] <= 6))
                                        {
                                            continue;
                                        }
                                        if (((col - colMod) >= pointx && (col - colMod) <= pointx + 4) && (row >= pointy && row <= pointy + 4))
                                            break;
                                    }
                                    if (j < alignmentSquaresCoords[_version - 1].Length)
                                        break;
                                }
                                if (i != alignmentSquaresCoords[_version - 1].Length)
                                    continue;
                            }
                            if (_version > 5 && (((col - colMod) > _matrixSize - 13 && (row < 7)) || (row > _matrixSize - 13 && ((col - colMod) < 7))))
                                continue;
                            if (data.Count != 0)
                                QRMatrix[row, col - colMod] = data.Dequeue();
                            if (((col - colMod) + row) % 2 == 0)
                            {
                                QRMatrix[row, col - colMod] = !QRMatrix[row, col - colMod];
                            }
                        }
                    }
                    direction = true;
                }
            }
            DrawMaskInfo();
        }

        private void DrawMaskInfo()
        {
            int x, y, i;
            for (x = 1, y = 9, i = 0; x < 9; x++, i++)
            {
                if (x == 7)
                {
                    i--;
                    continue;
                }
                QRMatrix[y, x] = maskCodes[_errorCorrectionLevel][i];
            }
            for (; y > 1; y--, i++)
            {
                if (y == 7)
                {
                    i--;
                    continue;
                }
                QRMatrix[y, x] = maskCodes[_errorCorrectionLevel][i];
            }
            for (x = 9, y = _matrixSize - 2, i = 0; y > _matrixSize - 9; y--, i++)
            {
                QRMatrix[y, x] = maskCodes[_errorCorrectionLevel][i];
            }
            for (x = _matrixSize - 9, y = 9; x < _matrixSize - 1; x++, i++)
            {
                QRMatrix[y, x] = maskCodes[_errorCorrectionLevel][i];
            }
        }

        public Bitmap GenerateImage(Queue<bool> data)
        {
            int x, y, pixelSize = 10;
            DrawData(data);
            picture = new Bitmap(_matrixSize * pixelSize, _matrixSize * pixelSize);
            Pen whitePen = new Pen(Color.Blue), blackPen = new Pen(Color.Black);
            SolidBrush brush = new SolidBrush(Color.Black);
            Graphics graphics = Graphics.FromImage(picture);
            graphics.Clear(Color.White);
            for (x = 0; x < Math.Sqrt(QRMatrix.Length); x++)
            {
                for (y = 0; y < Math.Sqrt(QRMatrix.Length); y++)
                {
                    if (QRMatrix[y, x])
                        graphics.FillRectangle(brush, x * pixelSize, y * pixelSize, pixelSize, pixelSize);
                    graphics.DrawRectangle(blackPen, x * pixelSize, y * pixelSize, pixelSize, pixelSize);
                }
            }
            return picture;
        }

        public void SaveImage(string path)
        {
            if (path.Length != 0)
            {
                picture.Save(path);
            }
        }
    }
}

