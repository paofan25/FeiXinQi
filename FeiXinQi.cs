using System.Drawing;

namespace 飞行棋;


class Program
{
    static void Main(string[] args){
        int[] a = new int[10];
        int w = 50;
        int h = 30;
        ConsolInit(w, h);
        E_SceneType nowSceneType = E_SceneType.start;
        while (true) {
            Console.Clear();
            switch (nowSceneType) {
                case E_SceneType.start:
                    Console.Clear();
                    StartScene(w, h, ref nowSceneType);
                    //按ws切换数字
                    break;
                case E_SceneType.game:
                    Console.Clear();
                    
                    GameScene(w, h, ref nowSceneType);
                    
                    break;
                case E_SceneType.end:
                    Console.Clear();
                    EndScene(w, h, ref nowSceneType);
                    break;
            }





        }


        static void ConsolInit(int w, int h){
            //光标的隐藏
            //舞台的大小}

            Console.CursorVisible = false;
            Console.SetWindowSize(w, h);
            Console.SetBufferSize(w, h);
        }

        static void ClearInfo(int h){
            Console.SetCursorPosition(2,h-6);
            Console.Write("                                 ");
            Console.SetCursorPosition(2,h-5);
            Console.Write("                                 ");
            Console.SetCursorPosition(2,h-4);
            Console.Write("                                 ");
            Console.SetCursorPosition(2,h-3);
            Console.Write("                                 ");
        }
        static void StartScene(int w, int h, ref E_SceneType nowSceneType){
            //设置光标位置
            Console.SetCursorPosition(w / 2 - 3, 10);
            Console.Write("飞行棋");
            int flag = 0;
            bool isCurrentScene = true;
            while (true) {
                //打印开始游戏
                Console.SetCursorPosition(w / 2 - 4, 14);
                Console.ForegroundColor =
                    flag == 0 ? ConsoleColor.Red : ConsoleColor.White;
                Console.Write("开始游戏");
                //打印进入游戏
                Console.SetCursorPosition(w / 2 - 4, 16);
                Console.ForegroundColor =
                    flag == 1 ? ConsoleColor.Red : ConsoleColor.White;
                Console.Write("退出游戏");
                //如果当前数字是0，文字变红，并能按任意键执行相应功能
                switch (Console.ReadKey(true).Key) {
                    case ConsoleKey.W:
                        flag--;
                        flag = flag <= 0 ? 0 : flag;
                        break;
                    case ConsoleKey.S:
                        flag++;
                        flag = flag >= 1 ? 1 : flag;
                        break;
                    case ConsoleKey.J:
                        if (flag == 0) {
                            nowSceneType = E_SceneType.game;
                            isCurrentScene = false;
                        }
                        else if (flag == 1) {
                            Environment.Exit(0);
                        }

                        break;
                }

                if (!isCurrentScene) {
                    break;
                }
            }
        }static void EndScene(int w, int h, ref E_SceneType nowSceneType){
            //设置光标位置
            Console.SetCursorPosition(w / 2 - 4, 10);
            Console.Write("游戏结束");
            int flag = 0;
            bool isCurrentScene = true;
            while (true) {
                //打印开始游戏
                Console.SetCursorPosition(w / 2 - 6, 14);
                Console.ForegroundColor =
                    flag == 0 ? ConsoleColor.Red : ConsoleColor.White;
                Console.Write("重新开始游戏");
                //打印进入游戏
                Console.SetCursorPosition(w / 2 - 4, 16);
                Console.ForegroundColor =
                    flag == 1 ? ConsoleColor.Red : ConsoleColor.White;
                Console.Write("退出游戏");
                //如果当前数字是0，文字变红，并能按任意键执行相应功能
                switch (Console.ReadKey(true).Key) {
                    case ConsoleKey.W:
                        flag--;
                        flag = flag <= 0 ? 0 : flag;
                        break;
                    case ConsoleKey.S:
                        flag++;
                        flag = flag >= 1 ? 1 : flag;
                        break;
                    case ConsoleKey.J:
                        if (flag == 0) {
                            nowSceneType = E_SceneType.game;
                            isCurrentScene = false;
                        }
                        else if (flag == 1) {
                            Environment.Exit(0);
                        }

                        break;
                }

                if (!isCurrentScene) {
                    break;
                }
            }
        }

        static void GameScene(int w, int h, ref E_SceneType nowSceneType){
            //绘制不变的信息
            DrawRed(w, h);
            //格子->格子结构体
            //格子结构体->地图结构体
            Map map = new Map(10, 2, 90);
            map.Draw();
            //绘制玩家
            Player player = new Player(0, E_PlayerType.Player);
            Player computer = new Player(0, E_PlayerType.Computer);
            bool isGameover = false;
            
            //游戏的循环
            while (true) {
                if (OneStep(w, h, ref player, ref computer, map, ref nowSceneType)) {
                    break;
                }

                if (OneStep(w, h, ref computer, ref player, map, ref nowSceneType)) {
                    break;
                }
                
                
                
                // Console.ReadKey();
            }
        }

        static bool OneStep(int w, int h, ref Player p, ref Player otherP, Map map,ref E_SceneType nowSceneType){
            Console.ReadKey(true);
            bool isGameover = Randomove(w, h, ref p, ref otherP, map);
            map.Draw();
            DrawPlayer(p, otherP, map);
            if (isGameover) {
                Console.ReadKey(true);
                nowSceneType = E_SceneType.end;
                
            }
            return isGameover;
        }
        static void DrawRed(int w, int h){
            Console.ForegroundColor = ConsoleColor.Red;
            //上方墙
            for (int i = 0; i <= w - 2; i += 2) {
                Console.SetCursorPosition(i, 1);
                Console.Write("■");
                Console.SetCursorPosition(i, h - 1);
                Console.Write("■");
                Console.SetCursorPosition(i, h - 6);
                Console.Write("■");
                Console.SetCursorPosition(i, h - 11);
                Console.Write("■");
            }

            //左右墙
            for (int i = 0; i <= h - 2; i++) {
                Console.SetCursorPosition(0, i);
                Console.Write("■");
                Console.SetCursorPosition(w - 2, i);
                Console.Write("■");
            }
            // Console.WriteLine("游戏界面"); 

            //使用说明

            Console.ForegroundColor = ConsoleColor.White;
            Console.SetCursorPosition(2, h - 11);
            Console.Write("□:普通格子");
            Console.SetCursorPosition(2, h - 10);
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("∥:暂停，一回合不动");
            Console.SetCursorPosition(26, h - 10);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("●:炸弹，倒退5格");
            Console.SetCursorPosition(2, h - 9);
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("■:时空隧道，随机倒退，暂停，换位置");
            Console.SetCursorPosition(2, h - 8);
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("★：玩家");
            Console.SetCursorPosition(12, h - 8);
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.Write("▲：AI");
            Console.SetCursorPosition(22, h - 8);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("◎:玩家和AI重合");
            //按随机按键开始游戏
            Console.SetCursorPosition(2, h - 6);
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("按任意键开始扔色子");
        }

        static void DrawPlayer(Player player, Player computer, Map map){
            if (player.nowIndex == computer.nowIndex) {
                Grid grid = map.grids[computer.nowIndex];
                Console.SetCursorPosition(grid.pos.x, grid.pos.y);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("◎");
            }
            else {
                player.Draw(map);
                computer.Draw(map);

            }
        }
        //扔色子函数
        static bool Randomove(int w, int h, ref Player p, ref Player otherP,Map map){
            //擦除之前显示的提示信息
            ClearInfo(h);
            Console.ForegroundColor=p.type==E_PlayerType.Player?ConsoleColor.Cyan:ConsoleColor.Magenta;
            
            if (p.isPause) {
                Console.SetCursorPosition(2,h-6);
                Console.Write("处于暂停点,{0}需要暂停一回合",
                    p.type == E_PlayerType.Player ? "你" : "电脑");
                Console.SetCursorPosition(2, h - 5);
                Console.Write("请按任意键让{0}开始扔色子",
                    p.type == E_PlayerType.Player ?   "电脑":"你");
                
                p.isPause = false;
                return false;
            }
            Random r = new Random();
            int randomNum = r.Next(1, 7);
            Console.SetCursorPosition(2, h - 6);
            Console.Write("{0}扔出的点数为:{1}",
                p.type == E_PlayerType.Player ? "你":"电脑"  ,randomNum);
            p.nowIndex += randomNum;
            if (p.nowIndex >= map.grids.Length - 1) {
                p.nowIndex = map.grids.Length - 1;
                Console.SetCursorPosition(2,h-5);
                if (p.type == E_PlayerType.Player) {
                    Console.Write("恭喜你率先到达终点");
                    
                }
                else {
                    Console.Write("很遗憾，电脑先到达了终点");
                }
                Console.SetCursorPosition(2,h-4);
                Console.Write("请按任意键结束游戏");
                return true;
            }
            else {
                Grid grid = map.grids[p.nowIndex];
                switch (grid.type) {
                    case E_GridType.Boom:
                        Console.SetCursorPosition(2, h - 5);

                        Console.Write("{0}踩到了炸弹，倒退7格",
                            p.type == E_PlayerType.Player ? "你" : "电脑", randomNum);
                        Console.SetCursorPosition(2, h - 4);
                        Console.Write("请按任意键让{0}开始扔色子",
                            p.type == E_PlayerType.Player ?  "电脑": "你", randomNum);
                        //爆炸退7格
                        p.nowIndex -= 7;
                        p.nowIndex =p.nowIndex < 0 ? 0 : p.nowIndex;
                        break;
                    case E_GridType.Normal:
                        Console.SetCursorPosition(2, h - 5);
                        Console.Write("{0}到了一个安全位置",
                            p.type == E_PlayerType.Player ? "你" : "电脑", randomNum);
                        Console.SetCursorPosition(2, h - 4);
                        Console.Write("请按任意键让{0}开始扔色子",
                            p.type == E_PlayerType.Player ?  "电脑": "你", randomNum);
                        //遇到正常格子,不执行操作
                        break;
                    case E_GridType.Pause:
                        Console.SetCursorPosition(2, h - 5);
                        Console.Write("{0}到了暂停位置，暂停一回合",
                            p.type == E_PlayerType.Player ? "你" : "电脑", randomNum);
                        Console.SetCursorPosition(2, h - 4);
                        Console.Write("请按任意键让{0}开始扔色子",
                            p.type == E_PlayerType.Player ? "电脑" : "你", randomNum);
                        //加一个标识,判断能不能扔色子
                        p.isPause = true;
                        break;
                    case E_GridType.Tunnel:
                        Console.SetCursorPosition(2, h - 5);
                        Console.Write("{0}走入了时空隧道",
                            p.type == E_PlayerType.Player ? "你" : "电脑", randomNum);
                        
                        randomNum=r.Next(1, 91);
                        if (randomNum < 30) {
                            p.nowIndex -= 7;
                            if (p.nowIndex < 0) {
                                p.nowIndex = 0;
                            }

                            Console.SetCursorPosition(2, h - 4);
                            Console.Write("触发倒退7格");
                        }else if (randomNum <= 60) {
                            p.isPause=true;
                            Console.SetCursorPosition(2, h - 4);
                            Console.Write("触发暂停");
                        }
                        else {
                            int temp=p.nowIndex;
                            p.nowIndex=otherP.nowIndex;
                            otherP.nowIndex=temp;
                            Console.SetCursorPosition(2, h - 4);
                            Console.Write("触发交换位置");
                        }

                        Console.SetCursorPosition(2, h - 3);
                        Console.Write("请按任意键让{0}开始扔色子",
                            p.type == E_PlayerType.Player ? "电脑": "你" , randomNum);
                        break;
                }

                //默认没有结束
                return false;
            }
            

        }
        /// <summary>
        /// 游戏场景枚举类型
        /// </summary>

    }
}

/// <summary>
/// 位置信息结构体，包含xy位置
/// </summary>
struct Vector2
{
    public int x;
    public int y;

    public Vector2(int x, int y){
        this.x = x;
        this.y = y;
    }
}

/// <summary>
/// 格子类型枚举
/// </summary>
enum E_GridType
{
    /// <summary>
    /// 普通格子
    /// </summary>
    Normal,

    /// <summary>
    /// 炸弹
    /// </summary>
    Boom,

    /// <summary>
    /// 暂停
    /// </summary>
    Pause,

    /// <summary>
    /// 时空隧道，随机倒退，暂停，换位置
    /// </summary>
    Tunnel,
}

struct Grid
{
    public E_GridType type;
    public Vector2 pos;

    public Grid(int x, int y, E_GridType type){
        pos.x = x;
        pos.y = y;
        this.type = type;
    }

    public void Draw(){
        Console.SetCursorPosition(pos.x, pos.y);
        switch (type) {
            case E_GridType.Boom:
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("\u25cf");
                break;
            case E_GridType.Normal:
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("□");
                break;
            case E_GridType.Pause:
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.Write("\u2225");
                break;
            case E_GridType.Tunnel:
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("■");
                break;


        }
    }
}

enum E_SceneType
{
    /// <summary>
    /// 开始场景
    /// </summary>
    start,

    /// <summary>
    /// 游戏场景
    /// </summary>
    game,

    /// <summary>
    /// 结束场景
    /// </summary>
    end,
}

struct Map
{
    public Grid[] grids;

    public Map(int x, int y, int num){

        grids = new Grid[num];
        // 用于位置改变计数的变量
        int indexX = 0;
        int indexY = 0;
        Random r = new Random();
        int randomNum;
        int stepNum = 2;
        for (int i = 0; i < num; i++) {
            randomNum = r.Next(0, 101);
            if (randomNum < 75 || i == 0 || i == num - 1) {
                grids[i].type = E_GridType.Normal;
            }
            else if (randomNum >= 75 && randomNum < 85) {
                grids[i].type = E_GridType.Boom;
            }
            else if (randomNum >= 85 && randomNum < 95) {
                grids[i].type = E_GridType.Pause;
            }
            else {
                grids[i].type = E_GridType.Tunnel;
            }

            grids[i].pos = new Vector2(x, y);
            if (indexX == 10) {
                y += 1;
                indexY++;
                if (indexY == 2) {
                    indexX = 0;
                    indexY = 0;
                    stepNum = -stepNum;
                }
            }
            else {
                x += stepNum;
                ++indexX;

            }
        }
    }

    public void Draw(){
        for (int i = 0; i < grids.Length; i++) {
            grids[i].Draw();
        }
    }
}

/// <summary>
/// 玩家类型枚举
/// </summary>
enum E_PlayerType
{
    /// <summary>
    /// 玩家
    /// </summary>
    Player,

    /// <summary>
    /// 电脑
    /// </summary>
    Computer,
}

struct Player{
    public E_PlayerType type;
    public int nowIndex;
    public bool isPause;
    public Player(int index, E_PlayerType type){
        this.type = type;
        nowIndex = index;
        isPause = false;
    }

    public void Draw(Map mapInfo){
        //得到地图上当前传入格子的信息
        Grid grid = mapInfo.grids[nowIndex];
        //设置位置
        Console.SetCursorPosition(grid.pos.x, grid.pos.y);
        //必须要先得到地图，才能知道在地图上的哪个格子
        //设置颜色，设置图标
        switch (type) {
            case E_PlayerType.Player:
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write("★");
                break;
            case E_PlayerType.Computer:
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.Write("▲");
                break;
        }
    }
}
