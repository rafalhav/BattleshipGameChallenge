using BattleshipGameCore;
using BattleshipGameCore.Configuration;
using BattleshipGameCore.Enums;
using BattleshipGameCore.Interfaces;
using BattleshipGameCore.Utilities;
using System.Drawing;

namespace BattleshipUI
{
    public partial class Form1 : Form
    {
        private int _columnCount = 10;
        private int _rowCount = 10;
        private List<int> _shipsConfiguration = new List<int>() { 4, 4, 5 };
        Board board;

        public Form1()
        {
            InitializeComponent();
            this.StartNewGame();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            board.CheckField(new Coordinates(e.ColumnIndex + 1, e.RowIndex + 1));
            DrawGridFromBoard(board, this._columnCount, this._rowCount);
            if (board.AreAllShipsSunk())
            {

                if (MessageBox.Show("You've sunk all ships. Do you want to start another battle?", "Congrats!", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    this.StartNewGame();
                }
            }
        }

        private void buttonStartNewGame_Click(object sender, EventArgs e)
        {
            this.StartNewGame();
        }

        private void DrawGridFromBoard(Board board, int columnCount, int rowCount)
        {
            dataGridView1.Rows.Clear();
            DataGridViewCellStyle styleEmpty = new DataGridViewCellStyle();
            styleEmpty.BackColor = Color.White;
            DataGridViewCellStyle styleMiss = new DataGridViewCellStyle();
            styleMiss.BackColor = Color.Gray;
            DataGridViewCellStyle styleSunk = new DataGridViewCellStyle();
            styleSunk.BackColor = Color.Red;
            styleSunk.ForeColor = Color.Red;
            DataGridViewCellStyle styleHit = new DataGridViewCellStyle();
            styleHit.BackColor = Color.Gray;


            for (int rowIndex = 1; rowIndex <= rowCount; rowIndex++)
            {
                DataGridViewRow row = new DataGridViewRow();

                for (int columnIndex = 1; columnIndex <= columnCount; columnIndex++)
                {
                    DataGridViewCellStyle cellStyle = styleEmpty;
                    DataGridViewCell cell = new DataGridViewButtonCell();
                    cell.Value = "";
                    switch (board.GetFieldStateByCoordinates(new Coordinates(columnIndex, rowIndex)))
                    {
                        case FieldStateEnum.Miss:
                            cell.Value = "X";
                            break;
                        case FieldStateEnum.Hit:
                            cellStyle = styleHit;
                            cell.Value = "#";
                            break;
                        case FieldStateEnum.Sunk:
                            cellStyle = styleSunk;
                            cell.Value = "#";
                            break;
                        default:
                            cellStyle = styleEmpty;
                            break;
                    }
                    cell.Style = cellStyle;
                    row.Cells.Add(cell);
                    row.HeaderCell.Value = rowIndex.ToString();

                }
                dataGridView1.Rows.Add(row);
            }
        }

        private void StartNewGame()
        {
            IShipsConfiguration shipsConfiguration = new ShipsConfiguration(_shipsConfiguration);
            IGameConfiguration gameConfiguration = new GameConfiguration(this._columnCount, this._rowCount);
            IShipsLocationProvider shipsLocationProvider = new ShipsRandomLocationProvider();
            board = new Board(gameConfiguration);
            board.PlaceShips(shipsConfiguration, shipsLocationProvider);
            dataGridView1.Columns.Clear();
            for (int columnIndex = 1; columnIndex <= this._columnCount; columnIndex++)
            {
                var column = new DataGridViewColumn(new DataGridViewButtonCell());
                column.Width = 28;
                column.Name = ((char)(64 + columnIndex)).ToString();
                dataGridView1.Columns.Add(column);
            }
            this.DrawGridFromBoard(board, _columnCount, _rowCount);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}