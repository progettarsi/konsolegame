using System;

namespace gioko
{
    class Program
    {
        //this is the matrix that defines the map
        public static char[,] pos = {{'.','.','.','.','.','.'},{'.','.','.','.','.','.'},{'.','.','.','.','.','.'},{'.','.','.','.','.','.'}};
        //this is the main character
        public static pg me = new pg("SHAdow");
        public static Random rnd = new Random();
        public static void Main(String[] args)
        {
            //these are random things for when u don't type a right command to work/sleep (this was the actual project before the 2d array) (not implemented/used)
            string[] answ = {"scusami?", "eh?", "non ho capito", "non credo sia corretto", "puoi ripetere?", "non è possibile", "perfavore ripeti", "non esiste ancora questo comando", "che strano"};
            List<pg> tot = new List<pg>();
            //sets ur position to the block [0,1] (the start)
            me.posizione[0] = 1;
            //the istance Paolo (P on the map, wich u can interact with)
            var Paolo = new pg("Paolo");
            //istance of obj in posizion (1,3)
            var tunnel = new obj(1,3);
            System.Console.WriteLine("Per informazioni sui comandi premi [I]");
            //string c = "";
            while(true)
            {
                //checks the experience u got to go next level
                if (me.lvl<pg.lvlmax)
                {
                    if (me.xp >= pg.mxp[me.lvl])
                    {
                        //level up
                        me.lvl++;
                        System.Console.WriteLine("!!Sei salito di livello!!");
                    }
                }
                
                //types the credits (saldo), the stamina and the level (livello)
                string a = "";
                System.Console.WriteLine("Saldo attuale: " + me.saldo.ToString());
                System.Console.WriteLine("Stamina: " + me.stamina.ToString());
                System.Console.WriteLine("Sei livello: " + me.lvl.ToString());
                /*System.Console.WriteLine("Cosa vuoi fare?");
                string s = Console.ReadLine(); (the first type of input i created to work/sleep)*/
                //sets every position of istances to the array
                pos[me.posizione[1],me.posizione[0]] = '0';
                pos[Paolo.posizione[1],Paolo.posizione[0]] = 'P';
                pos[tunnel.posizione[1],tunnel.posizione[0]] = '=';
                //creates the string a wich is a rappresentation of the map in string a
                for(int i = 0; i<pos.GetLength(0);i++)
                {
                    for(int j = 0; j<pos.GetLength(1);j++)
                    {
                        a = a + pos[i,j];
                    }
                    a = a + 
                    "\n";
                }
                //output of the map
                System.Console.WriteLine(a);
                //INPUT for movement and actions
                //x is the var to set in the input, it rappreents the key pressed and on it's value the function input do things on other functions
                var x = Console.ReadKey();
                try
                {
                    Console.Clear();
                    me.input(x);
                    //grafica
                    for(int i = 0; i<pos.GetLength(0);i++)
                    {
                        for(int j = 0; j<pos.GetLength(1);j++)
                        {
                            pos[i,j]='.';
                        }
                    }
                    pos[me.posizione[1],me.posizione[0]] = '0';
                }
                catch(IndexOutOfRangeException maplimit)
                {
                    //sets map limit
                    string[] output = {"Non puoi uscire dai confini della mappa!", "Di qui non si va!", "Stai sbagliando direzione...", "Di qua non tu puoi andare, giovane Padowan", "NO, non si passa di qua!"};
                    System.Console.WriteLine(output[rnd.Next(0,4)]);
                }

                /*if (s=="lavora")
                    me.work();
                else if (s=="dormi")
                    me.sleep();
                else if (s=="exit")
                    break;
                else
                {
                    Console.Clear();
                    int i = rnd.Next(0,answ.Length);
                    System.Console.WriteLine(answ[i] + "\n");
                }*/
            }
        }
    }
    
    public class pg
    {
        //pg things, like name, stamina, position(posizione), credits(saldo), level(lvl) and experience(xp) (not implemented)
        public string name;
        
        public static Random rnd = new Random();
        public int stamina;
        public int[] posizione = {0,0};
        public int saldo;
        public int lvl;
        public int xp;
        public static int lvlmax = 3;
        static int[] stmstk = {10,20,25,30};    //these are value of stamina that regulates the ammount of stamina that u get/loose (not implemented)
        static int[] sldstk = {20,25,30,40};    //these are value of credits u get for work (not implemented)
        public static int[] mxp = {70,140,260}; //these are the maximum experience to get to go on the next level (not implemented)
        public static List<pg> tot = new List<pg>{};    //list of istances of pg
        public static int count = 0;    //counter of istances created

        public pg(string n)
        {
            name = n;
            saldo = 600;
            stamina = 100;
            lvl = 0;
            xp = 0;
            count++;
            tot.Add(this);
            //adds every instance to a list of instances
        }

        public void work()
        {
            Console.Clear();
            //check if there is enough stamina to work and then gives u credits(saldo) (not implemented in actual state)
            if (stamina>(stmstk[lvl]/2)-1)
            {
                stamina = stamina - stmstk[lvl]/2;
                saldo = saldo + sldstk[lvl];
                xp = xp + stmstk[lvl];
                Console.WriteLine(name + " ha lavorato, +" + sldstk[lvl].ToString());
            }
            else System.Console.WriteLine("Non puoi lavorare, non hai abbastanza stamina");
        }
        public void move (ConsoleKeyInfo x)
        {
            Console.Clear();
            //check if has enough stamina to move
            if(stamina>1)
            {
                int[] vpos = (this.posizione).Clone() as int[];
                int[] npos = this.posizione;
                //movement with arrows and WASD
                if (x.Key==ConsoleKey.A || x.Key==ConsoleKey.LeftArrow)
                {
                    if (posizione[0]<=0)
                    {
                        throw new IndexOutOfRangeException();
                    }
                    else 
                    {
                        npos[0]--;
                        stamina--;
                    }
                }
                else if (x.Key==ConsoleKey.RightArrow||x.Key==ConsoleKey.D)
                {
                    if(posizione[0]>=Program.pos.GetLength(1)-1)
                    {
                        throw new IndexOutOfRangeException();
                    }
                    else 
                    {
                        npos[0]++;
                        stamina--;
                    }
                }
                else if (x.Key==ConsoleKey.UpArrow || x.Key==ConsoleKey.W)
                {
                    if(posizione[1]<=0)
                    {
                        throw new IndexOutOfRangeException();
                    }
                    else 
                    {
                        npos[1]--;
                        stamina--;
                    }
                }
                else if (x.Key==ConsoleKey.DownArrow || x.Key==ConsoleKey.S)
                {
                    if(posizione[1]>=Program.pos.GetLength(0)-1)
                    {
                        throw new IndexOutOfRangeException();
                    }
                    else 
                    {
                        npos[1]++;
                        stamina--;
                    }
                }
                //check if there are obstacles
                if (Program.pos[npos[1],npos[0]]!='.')
                {
                    this.posizione = vpos;
                    stamina++;
                }
                else this.posizione = npos;
                this.xp++;
            }
            else System.Console.WriteLine("Non puoi muoverti se non hai stamina, premi [B] per riposarti");
        }
        //the input function
        public void input(ConsoleKeyInfo x)
        {
            //check the key input, so if is [B] it calls sleep() function
            if(x.Key==ConsoleKey.B)
            {
                this.sleep();
            }
            //if Key=X check if u can interact with someone (1) and if so it interact using interact(me,k) with k (2)
            else if (x.Key==ConsoleKey.X)
            {
                //(1)
                foreach(pg k in pg.tot)
                {
                    //1.1 ignores the protagonist
                    if (k != Program.me)
                    {
                        //1.2 check if the distance is enough to interact
                        if (pg.dis(Program.me,k)<2)
                        {
                            //(2)
                            this.interact(k);
                            break;
                        }
                    }
                }
            }
            //if Key=WASD or Directional Arrow and it calls the function move if so
            else if (x.Key==ConsoleKey.A || x.Key==ConsoleKey.LeftArrow || x.Key==ConsoleKey.RightArrow||x.Key==ConsoleKey.D||x.Key==ConsoleKey.UpArrow || x.Key==ConsoleKey.W||x.Key==ConsoleKey.DownArrow || x.Key==ConsoleKey.S)
            {
                this.move(x);
            }
            //if Key=I it returns information about the game, like a little tutorial
            else if(x.Key==ConsoleKey.I)
            {
                System.Console.WriteLine("!!Premi [Enter] per informazioni aggiuntive!!\nPer muoverti puoi usare [W] [A] [S] [D] o le Freccette Direzionali.\nPremi [B] per riposarti quando non hai stamina.\nPer interagire con i pg, usa [X].\nPer uscire premi [Esc].\n");
            }
            else if (x.Key==ConsoleKey.Escape)
            {
                Environment.Exit(0);
            }
        }

        //calculates the distance of istances of pg (userful to check interaction)
        static public int dis(pg a, pg b)
        {
            int dis = Convert.ToInt32(Math.Sqrt(Math.Pow((a.posizione[0]-b.posizione[0]),2)+Math.Pow((a.posizione[1]-b.posizione[1]),2)));
            return dis;
        }
        //interact function that returns the message below with the pg b
        public void interact(pg b)
        {
            System.Console.WriteLine("Hai interagito con: " + b.name);
        }
        //sleep function that recharges ur stamina, so u can move and in future do actions
        public void sleep()
        {
            Console.Clear();
            if (stamina<81)
            {
                stamina = stamina + stmstk[lvl];
                for(int i = 0;i<2;i++)
                {
                    Console.WriteLine("...stai dormendo");
                    Thread.Sleep(400);
                    Console.Clear();
                    Console.WriteLine("...stai dormendo.");
                    Thread.Sleep(400);
                    Console.Clear();
                    Console.WriteLine("...stai dormendo..");
                    Thread.Sleep(400);
                    Console.Clear();
                    Console.WriteLine("...stai dormendo...");
                    Thread.Sleep(500);
                    Console.Clear();
                }
                Console.Clear();
                Console.WriteLine(name + " ha dormito");
                }
            else if (stamina<100)
            {
                stamina = 100;
                for(int i = 0;i<2;i++)
                {
                    Console.WriteLine("...stai dormendo");
                    Thread.Sleep(400);
                    Console.Clear();
                    Console.WriteLine("...stai dormendo.");
                    Thread.Sleep(400);
                    Console.Clear();
                    Console.WriteLine("...stai dormendo..");
                    Thread.Sleep(400);
                    Console.Clear();
                    Console.WriteLine("...stai dormendo...");
                    Thread.Sleep(500);
                    Console.Clear();
                }
                Console.Clear();
                Console.WriteLine(name + " ha dormito");
            }
            else 
            {
                string[] output = {"Hai già il massimo della stamina!", "Non puoi andare oltre il limite", "Chi dorme non piglia pesci", "Non dormire così tanto, è inutile", "La stamina è già al massimo!"};
                System.Console.WriteLine(output[rnd.Next(0,4)]);
            }
            
        }
    }
    //the class obj rapresents an obstacle like
    public class obj
    {
        //it only has position property
        public int[] posizione = new int[2];
        public obj(int x, int y)
        {
            this.posizione[1] = x;
            this.posizione[0] = y;
        }
    }
}
