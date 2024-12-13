using Terminal.Gui;

namespace MarsRover.UILayerTG.Utils
{
    public class GridView : View
    {

        public Dictionary<XYPosition, Label> gridLabels = new Dictionary<(int xAxis, int yAxis), Label>();

        public GridView(int y, string[,] myGrid)
        {
            X = 0;
            Y = y;
            Width = 4 + myGrid.GetLength(1) - 5;
            Height = 4 + myGrid.GetLength(0) - 5;

            int startX = 2;
            int startY = 2;
            for (int i = myGrid.GetLength(0) - 6; i >= 5; i--)
            {
                for (int j = 5; j < myGrid.GetLength(1) - 5; j++)
                {
                    
                    var label = new Label(myGrid[i, j])
                    {
                        X = startX + j - 5,
                        Y = startY + i - 5,
                        Height = 1,
                        ColorScheme = GetColor(myGrid[i, j])
                    };

                    Add(label);
                    gridLabels.Add((j, i), label);

                }
            }
        }


        public ColorScheme GetColor(string itemToColor) => itemToColor switch
        {
            "⣫" => new ColorScheme
            {
                Normal = new Terminal.Gui.Attribute(Terminal.Gui.Color.Magenta, Terminal.Gui.Color.Black)
            },
            "⡺" => new ColorScheme
            {
                Normal = new Terminal.Gui.Attribute(Terminal.Gui.Color.Magenta, Terminal.Gui.Color.Black)
            },
            "⠿" => new ColorScheme
            {
                Normal = new Terminal.Gui.Attribute(Terminal.Gui.Color.Brown, Terminal.Gui.Color.Black)
            },
            "⣤" => new ColorScheme
            {
                Normal = new Terminal.Gui.Attribute(Terminal.Gui.Color.Brown, Terminal.Gui.Color.Black)
            },
            "⟡" => new ColorScheme
            {
                Normal = new Terminal.Gui.Attribute(Terminal.Gui.Color.BrightMagenta, Terminal.Gui.Color.Black)
            },
            "🝆" => new ColorScheme
            {
                Normal = new Terminal.Gui.Attribute(Terminal.Gui.Color.BrightRed, Terminal.Gui.Color.Black)
            },
            " " => new ColorScheme
            {
                Normal = new Terminal.Gui.Attribute(Terminal.Gui.Color.Black, Terminal.Gui.Color.Black)
            },
            "⊕" => new ColorScheme
            {
                Normal = new Terminal.Gui.Attribute(Terminal.Gui.Color.BrightGreen, Terminal.Gui.Color.Black)
            },
            _ => new ColorScheme
            {
                Normal = new Terminal.Gui.Attribute(Terminal.Gui.Color.BrightCyan, Terminal.Gui.Color.Black)
            }
        };


        public void MoveObjectTo(ulong Key, XYPosition oldPos, XYPosition newPos, string oldObj)
        {
            XYPosition key = (newPos.xAxis + 7, newPos.yAxis + 7);
            gridLabels[(oldPos.xAxis + 7, oldPos.yAxis + 7)].Text = oldObj;
            gridLabels[(oldPos.xAxis + 7, oldPos.yAxis + 7)].ColorScheme = GetColor(oldObj);
            if (gridLabels.ContainsKey(key))
            {
                gridLabels[key].Text = $"{Key}";
                gridLabels[key].ColorScheme = GetColor("1");
            }
        }



    }



}


