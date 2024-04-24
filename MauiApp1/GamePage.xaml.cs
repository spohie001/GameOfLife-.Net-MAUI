
using CommunityToolkit.Maui;
using MauiApp1.src;
using Cell = MauiApp1.src.Cell;

namespace MauiApp1;

public partial class GamePage : ContentPage
{
    int boardSize;
    int margin = 50;
    (int y, int x) startMargin = (300, 150);
    Button[,] stateButtons;
    List<Button> editButtons = new List<Button>();
    List<Button> autoButtons = new List<Button>();
    List<Button> singleButtons = new List<Button>();
    bool isAutomaticOn = false;
    Mode gameMode = Mode.NONE;
    Board board;
    int genCtr = 0;
    int aliveCtr = 0;
    int deadCtr = 0;
    List<Board> previousBoards = new List<Board>();
    enum Mode
    {
        AUTOMATIC,
        SINGLE,
        EDIT,
        NONE
    }
    public GamePage(int boardSize)
    {
        this.boardSize = boardSize;
        deadCtr = this.boardSize * this.boardSize;
        InitializeComponent();
        CreateStateButtons();
        CreateFunctionalButtons();
        UpdateFirstBoard();
        updateStatistics();
    }
    public GamePage(string data)
    {
        LoadBoard(data);
        InitializeComponent();
        CreateStateButtons();
        UpdateStateButton();
        CreateFunctionalButtons();
        updateStatistics();
    }
    private void LoadBoard(string data)
    {
        string[] tokens = data.Split(' ');
        this.boardSize = int.Parse(tokens[0]);
        deadCtr = 0;
        Cell[,] cells = new Cell[this.boardSize, this.boardSize];
        for (int i = 0; i < boardSize; i++)
        {
            for (int j = 0; j < boardSize; j++)
            {
                string t = tokens[i * boardSize + j + 1];
                if (t == "1")
                {
                    cells[i, j] = new Cell(true, i, j);
                }
                else
                {
                    cells[i, j] = new Cell(false, i, j);
                    deadCtr++;
                }
            }
        }
        this.board = new Board(cells, this.boardSize);
    }
    private void UpdateBoard()
    {
        Cell[,] cells = new Cell[this.boardSize, this.boardSize];
        for (int i = 0; i < boardSize; i++)
        {
            for (int j = 0; j < boardSize; j++)
            {
                Button b = this.stateButtons[i, j];
                if (b.Background == Brush.ForestGreen)
                {
                    cells[i, j] = new Cell(true, i, j);
                }
                else
                {
                    cells[i, j] = new Cell(false, i, j);
                }
            }
        }
        this.board = new Board(cells, this.boardSize);
    }
    private void UpdateFirstBoard()
    {
        Cell[,] cells = new Cell[this.boardSize, this.boardSize];
        for (int i = 0; i < boardSize; i++)
        {
            for (int j = 0; j < boardSize; j++)
            {
                Button b = this.stateButtons[i, j];
                if (b.Background == Brush.ForestGreen)
                {
                    cells[i, j] = new Cell(true, i, j);
                }
                else
                {
                    cells[i, j] = new Cell(false, i, j);
                }
            }
        }
        this.board = new Board(cells, this.boardSize);
    }
    public void updateStatistics()
    {
        string numOfCells = (this.boardSize * this.boardSize).ToString();
        string generation = "current generation: " + this.genCtr.ToString();
        string alive = "currently alive: " + this.aliveCtr.ToString() + " / " + numOfCells;
        string dead = "currently dead: " + this.deadCtr.ToString() + " / " + numOfCells;
        Statistics.Text = generation + "\n" + alive + "\n" + dead;
    }
    private void UpdateStateButton()
    {
        this.aliveCtr = 0;
        this.deadCtr = 0;
        for (int i = 0; i < boardSize; i++)
        {
            for (int j = 0; j < boardSize; j++)
            {
                Button b = this.stateButtons[i, j];
                if (this.board.cells[i, j].getIsAlive())
                {
                    b.Background = Brush.ForestGreen;
                    this.aliveCtr++;
                }
                else
                {
                    b.Background = Brush.Black;
                    this.deadCtr++;

                }
            }
        }
        updateStatistics();
    }
    public void OnClickChangeState(object sender, EventArgs e)
    {
        Button s = (Button)sender;
        if (s.Background == Brush.ForestGreen)
        {
            s.Background = Brush.Black;
        }
        else
        {
            s.Background = Brush.ForestGreen;
        }
    }
    public void CreateNewFunctionalButton(int column, int row, string txt,
                                                List<Button> buttonsList,
                                                EventHandler onClick)
    {
        Button button = new Button();
        button.Text = txt;
        button.IsEnabled = false;
        button.IsVisible = false;
        button.Clicked += onClick;
        buttonsList.Add(button);
        buttonGrid.Add(button,column,row);

    }
    public void CreateEditModeButtons()
    {
        //next generation
        CreateNewFunctionalButton(0,3, "NEXT",
            editButtons, new EventHandler(PlayNextGeneration));

        //previous generation
        CreateNewFunctionalButton(0, 4, "PREVIOUS",
            editButtons, new EventHandler(GetPreviousGeneration));

        //save game
        CreateNewFunctionalButton(0, 5, "SAVE",
            editButtons, new EventHandler(SaveGame));
    }
    public void CreateAutoModeButtons()
    {
        //stop
        CreateNewFunctionalButton(0, 3, "STOP",
            autoButtons, new EventHandler(StopAutomaticGame));

        //start
        CreateNewFunctionalButton(0, 4, "START",
            autoButtons, new EventHandler(StartAutomaticGame));
    }
    public void CreateSingleModeButtons()
    {
        //next generation
        CreateNewFunctionalButton(0, 3, "NEXT",
            singleButtons, new EventHandler(PlayNextGeneration));

        //previous generation
        CreateNewFunctionalButton(0, 4, "PREVIOUS",
            singleButtons, new EventHandler(GetPreviousGeneration));
    }
    public void CreateFunctionalButtons()
    {
        CreateEditModeButtons();
        CreateAutoModeButtons();
        CreateSingleModeButtons();
    }
    public async void StartAutomaticGame(object sender, EventArgs e)
    {
        this.isAutomaticOn = true;
        while (isAutomaticOn && this.gameMode == Mode.AUTOMATIC)
        {
            NextGeneration();
            await Task.Delay(1000);
        }
    }
    public void StopAutomaticGame(object sender, EventArgs e)
    {
        this.isAutomaticOn = false;
    }
    public void PlayNextGeneration(object sender, EventArgs e)
    {
        UpdateBoard();
        NextGeneration();
    }
    public void NextGeneration()
    {
        genCtr++;
        previousBoards.Add(this.board);
        updateStatistics();
        UpdateBoard();
        board.nextGeneration();
        UpdateStateButton();
    }
    public void GetPreviousGeneration(object sender, EventArgs e)
    {
        if (this.previousBoards.Count > 1)
        {
            this.genCtr--;
            updateStatistics();
            int prevIdx = previousBoards.Count - 1;
            this.board = this.previousBoards.ElementAt(prevIdx);
            this.previousBoards.RemoveAt(prevIdx);
            UpdateStateButton();
        }
    }
    public void SaveGame(object sender, EventArgs e)
    {
        UpdateBoard();
        SaveManager sm = new SaveManager();
        sm.Save(this.board, this.boardSize);
    }

    public void CreateStateButtons()
    {
        stateButtons = new Button[boardSize, boardSize];
        for (int i = 0; i < boardSize; i++)
        {
            for (int j = 0; j < boardSize; j++)
            {
                Button button = new Button();
                int newY = this.startMargin.y;
                int newX = this.startMargin.x;
                Thickness newMargin = new Thickness(0, 0, newX, newY);
                button.Margin = newMargin;
                this.startMargin.x -= this.margin;
                button.IsEnabled = false;
                button.HeightRequest = 10;
                button.WidthRequest = 10;
                button.Background = Brush.Black;
                button.Clicked += new EventHandler(OnClickChangeState);

                this.stateButtons[i, j] = button;
                this.buttonGrid.Add(button);
            }
            this.startMargin.y -= this.margin;
            this.startMargin.x += this.margin * boardSize;
        }
    }

        void EnableDisableDisableButtons(List<Button> enable,
            List<Button> disable1, List<Button> disable2)
        {
            foreach (var button in enable)
            {
                button.IsVisible = true;
                button.IsEnabled = true;
            }
            foreach (var button in disable1)
            {
                button.IsVisible = false;
                button.IsEnabled = false;
            }
            foreach (var button in disable2)
            {
                button.IsVisible = false;
                button.IsEnabled = false;
            }
        }
        void UpdateEnabledStateButtons(bool enabled)
        {
            foreach (var button in this.stateButtons)
            {
                button.IsEnabled = enabled;
            }
        }
        void UpdateEnabledButtons()
        {
            if (this.gameMode == Mode.AUTOMATIC)
            {
                EnableDisableDisableButtons(autoButtons, editButtons, singleButtons);
                UpdateEnabledStateButtons(false);
            }
            else if (this.gameMode == Mode.SINGLE)
            {
                EnableDisableDisableButtons(singleButtons, autoButtons, editButtons);
                UpdateEnabledStateButtons(false);
            }
            else if (this.gameMode == Mode.EDIT)
            {
                EnableDisableDisableButtons(editButtons, singleButtons, autoButtons);
                UpdateEnabledStateButtons(true);
            }
        }
    void ChangeMode(object sender, EventArgs e)
    {
            int index = modePicker.SelectedIndex;
            if (index == 0)
            {
                this.gameMode = Mode.AUTOMATIC;
            }
            else if (index == 1)
            {
                this.gameMode = Mode.SINGLE;
            }
            else if (index == 2)
            {
                this.gameMode = Mode.EDIT;
            }
            UpdateEnabledButtons();
        }
    }