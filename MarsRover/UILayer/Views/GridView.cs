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
            Width = 4 + myGrid.GetLength(1);
            Height = 4 + myGrid.GetLength(0);

            int startX = 2;
            int startY = 2;
            for (int i = myGrid.GetLength(0) - 1; i >= 0; i--)
            {
                for (int j = 0; j < myGrid.GetLength(1); j++)
                {
                    
                    var label = new Label()
                    {
                        X = startX + j,
                        Y = startY + i,
                        Height = 1,
                        Text = myGrid[i, j],
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
            "▛" => new ColorScheme
            {
                
                //Normal = new Terminal.Gui.Attribute(Color.FromRgb(123, 45, 200), Terminal.Gui.Color.Black)
            },

            "▞" => new ColorScheme
            {
                Normal = new Terminal.Gui.Attribute(Terminal.Gui.Color.Magenta, Terminal.Gui.Color.Black)
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
            gridLabels[(oldPos.xAxis + 2, oldPos.yAxis + 2)].Text = oldObj;
            gridLabels[(oldPos.xAxis + 2, oldPos.yAxis + 2)].ColorScheme = GetColor(oldObj); 

            gridLabels[(newPos.xAxis + 2, newPos.yAxis + 2)].Text = $"{Key}";
            gridLabels[(newPos.xAxis + 2, newPos.yAxis + 2)].ColorScheme = GetColor("1");
        }



    }



}


