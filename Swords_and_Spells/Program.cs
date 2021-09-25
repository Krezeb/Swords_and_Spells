using System;

namespace Swords_and_Spells
{
    class Program
    {
        static void Main(string[] args)
        {
            Random rndNum = new Random(); //Skapar en "random" variabel (rnd) som används inom loopen
            string playerOneName = "Player 1";
            float maxPlayerOneHealth = 30, maxPlayerTwoHealth = 30;
            string[,] actionArray = new string[,] { { "1.", "2.", "3." }, { "Attack", "Big Swing", "Heal" } };
            string[] enemyArray = new[] { "Crazed Bandit", "Hungry Ogre", "Shambling Ghoul" };
            int enemyActiveRNG = rndNum.Next(0, enemyArray.Length);
            string playerTwoName = enemyArray[enemyActiveRNG];
            int menuBoxWidth = 30;
            string actionBoxHor = new('-', menuBoxWidth);
            string actionBoxHorLow = ($"{actionBoxHor}\n");
            float playerOneCurrentHealth = maxPlayerOneHealth;
            float playerTwoCurrentHealth = maxPlayerTwoHealth;
            int enemyDmgLastRound = 0;
            bool enemyTurn = false;
            bool playerTurn = true;
            bool isRunning = true;
            while (isRunning)
            {
                string action;
                float tempEnemyHP = 0, tempPlayerHP = 0;
                int baseDmg, critMulti, critDmg, enemyBaseDmg, enemyCritMulti, enemyCritDmg;
                string playerOneCurrentHealthStr = playerOneCurrentHealth.ToString();
                string playerTwoCurrentHealthStr = playerTwoCurrentHealth.ToString();
                string playerOneStats = ($"{playerOneName} (HP: {playerOneCurrentHealthStr})");
                string playerTwoStats = ($"{playerTwoName} (HP: {playerTwoCurrentHealthStr})");
                String headerFormat = String.Format("{0,-30} {1,30}\n\n", playerOneStats, playerTwoStats);
                //Random rndNum = new Random(); //Skapar en "random" variabel (rnd) som används inom loopen
                int rndNumPlayer = rndNum.Next(3, 7);    //En random siffra som spelaren kan använda mellan 3-6
                int rndNumCritplayer = rndNum.Next(0, 4);    //En random siffra som spelaren kan använda i Crits mellan 0-3
                int rndNumEnemy = rndNum.Next(3, 7);     //En random siffra som fienden kan använda mellan 3-6
                int rndNumCritEnemy = rndNum.Next(0, 4);    //En random siffra som fienden kan använda i Crits, mellan 0-3
                int rndChoice = rndNum.Next(1, 3); //Fiendens attack blir antingen val 1 eller 2
                int rndPlayerHeal = rndNum.Next(1, 11); //Spelaren healar sig själv för 1-10 HP
                //Siffrorna i parenteserna är vilka siffror som ska väljas mellan.
                //OBS ANDRA SIFFRAN MÅSTE VARA 1 HÖGRE (INDEX BÖRJAR PÅ 0)
                string turnMessagePlayer = ($"It's {playerOneName}'s Turn");
                string turnTrackerPlayer = ($"{playerTwoName}'s last attack: {enemyDmgLastRound}");
                string messageFormatPlayer = String.Format("{0,-30} {1,30}\n\n", turnMessagePlayer, turnTrackerPlayer);
                string turnMessageEnemy = ($"It's {playerTwoName}'s Turn");
                string turnTrackerEnemy = ($"{playerTwoName}'s last attack: {enemyDmgLastRound}");
                string messageFormatEnemy = String.Format("{0,-30} {1,30}\n\n", turnMessageEnemy, turnTrackerEnemy);
                baseDmg = rndNumPlayer;         //Base attack damage for Player between 3-6
                critMulti = rndNumCritplayer;   //Crit multiplayer for player betweem 0-3
                critDmg = baseDmg * critMulti;  //Total crit damage for player 
                enemyBaseDmg = rndNumEnemy;     // Base attack damage for enemy
                enemyCritMulti = rndNumCritEnemy;// Crit multiplyer for enemy
                enemyCritDmg = enemyBaseDmg * enemyCritMulti; //Total crit damage for enemy
                //-------------------------------------------------
                Console.Clear();
                Console.WriteLine(headerFormat);
                Console.WriteLine($"{messageFormatPlayer}");
                Console.WriteLine("\n\n");
                Console.WriteLine(actionBoxHor);
                for (int i = 0; i < actionArray.Length / 2; ++i)
                {
                    string actionArrayStr = ($"{actionArray[0, i]}: {actionArray[1, i]}");
                    String actionFormat = String.Format("| {0,-26} |", actionArrayStr); //{0, ZZ} ZZ = menuBoxWidth - 4
                    Console.WriteLine(actionFormat);
                }
                Console.WriteLine(actionBoxHorLow);
                Console.WriteLine("Enter a number to perform an attack.. ");
                //-------------------------------------------------
                //Console.WriteLine("Debug- rndNumPlayer: " + rndNumPlayer);
                //Console.WriteLine("Debug- rndNumCritplayer: " + rndNumCritplayer);
                //Console.WriteLine("Debug- rndNumEnemy: " + rndNumEnemy);
                //Console.WriteLine("Debug- rndNumCritEnemy " + rndNumCritEnemy);
                //Console.WriteLine("Debug- rndChoice: " + rndChoice);
                //Console.WriteLine("Debug- rndPlayerHeal: " + rndPlayerHeal);
                //-------------------------------------------------
                action = Console.ReadLine();
                while (playerTurn)
                {
                    bool playerAttack = true;
                    while (playerAttack)
                    {
                        Console.Clear();
                        Console.WriteLine(headerFormat);
                        Console.WriteLine($"{messageFormatPlayer}");
                        if (action == "1") //If player makes an Attack
                        {
                            Console.WriteLine("You strike at the enemy with your weapon!");
                            Console.WriteLine($"The creature takes {baseDmg} points of damage!\n");
                            playerTwoCurrentHealth = playerTwoCurrentHealth - baseDmg;
                            tempEnemyHP = playerTwoCurrentHealth;
                            Console.ForegroundColor = ConsoleColor.DarkGray;//Greys out action menu for UI reasons
                            Console.WriteLine(actionBoxHor);
                        }
                        else if (action == "2") //If player makes a Critical Strike
                        {
                            Console.WriteLine("You attempt a powerful but ungainly overhead swing.");
                            Console.WriteLine($"The creature is hit for ({baseDmg} * {critMulti}) = {critDmg} points of damage!;\n");
                            playerTwoCurrentHealth = playerTwoCurrentHealth - critDmg;
                            tempEnemyHP = playerTwoCurrentHealth;
                            Console.ForegroundColor = ConsoleColor.DarkGray;//Greys out action menu for UI reasons
                            Console.WriteLine(actionBoxHor);
                        }
                        else if (action == "3") //If player Heals Damage
                        {
                            Console.WriteLine("You focus your arcane energies and a brilliant light erupts from your body.");
                            Console.WriteLine($"You are Healed for {rndPlayerHeal} points of damage!;\n");
                            playerOneCurrentHealth = playerOneCurrentHealth + rndPlayerHeal;
                            tempPlayerHP = playerOneCurrentHealth;
                            Console.ForegroundColor = ConsoleColor.DarkGray;//Greys out action menu for UI reasons
                            Console.WriteLine(actionBoxHor);
                        }
                        for (int i = 0; i < actionArray.Length / 2; ++i)//Shows action menu
                        {
                            string actionArrayStr = ($"{actionArray[0, i]}: {actionArray[1, i]}");
                            String actionFormat = String.Format("| {0,-26} |", actionArrayStr); //{0, ZZ} ZZ = menuBoxWidth - 4          
                            Console.WriteLine(actionFormat);
                        }
                        Console.WriteLine(actionBoxHorLow);
                        Console.ResetColor();
                        Console.WriteLine("Press Enter to end turn.. ");
                        Console.ReadLine();
                        playerAttack = false;
                        
                    }
                    if (playerTwoCurrentHealth <= 0) //Win Condition
                    {
                        Console.Clear();
                        Console.WriteLine(headerFormat);
                        Console.WriteLine("\n");
                        Console.WriteLine($"Player {playerOneName} Won!");
                        Console.WriteLine("\n\n");
                        Console.ForegroundColor = ConsoleColor.DarkGray; //Greys out action menu for UI reasons
                        Console.WriteLine(actionBoxHor);
                        for (int i = 0; i < actionArray.Length / 2; ++i) //Shows action menu
                        {
                            string actionArrayStr = ($"{actionArray[0, i]}: {actionArray[1, i]}");
                            String actionFormat = String.Format("| {0,-26} |", actionArrayStr); //{0, ZZ} ZZ = menuBoxWidth - 4
                            Console.WriteLine(actionFormat);
                        }
                        Console.WriteLine(actionBoxHorLow);
                        Console.ResetColor();
                        Console.WriteLine("Press Enter to Quit.. ");
                        Console.ReadLine();
                        playerTurn = false;
                        isRunning = false;
                        //System.Environment.Exit(0); //Kan användas för att stoppa en app direkt
                    }
                    else
                    {
                        playerTurn = false;
                        enemyTurn = true;
                    }
                }
                while (enemyTurn)
                {
                    String headerTempFormat;
                    if (action == "3") //If healed
                    {
                        string tempEnemyHPStr = playerTwoCurrentHealth.ToString();
                        string tempPlayerHPStr = tempPlayerHP.ToString();
                        string playerOneTempStats = ($"{playerOneName} (HP: {tempPlayerHP})");
                        headerTempFormat = String.Format("{0,-30} {1,30}\n\n", playerOneTempStats, playerTwoStats);
                    }
                    else //If attacked
                    {
                        string tempEnemyHPStr = tempEnemyHP.ToString();
                        string playerOneTempStats = ($"{playerOneName} (HP: {playerOneCurrentHealth})");
                        string playerTwoTempStats = ($"{playerTwoName} (HP: {tempEnemyHP})");
                        headerTempFormat = String.Format("{0,-30} {1,30}\n\n", playerOneTempStats, playerTwoTempStats);
                    }
                    bool enemyAttack = true;
                    while (enemyAttack)
                    {
                        Console.Clear();
                        Console.WriteLine($"{headerTempFormat}");
                        Console.WriteLine($"{messageFormatEnemy}");
                        System.Threading.Thread.Sleep(500);
                        if (rndChoice == 1) // If enemy Choice = 1 (Attack)
                        {
                            Console.WriteLine($"The {playerTwoName} strikes towards you!");
                            Console.WriteLine($"The creature does {enemyBaseDmg} points of damage!\n");
                            playerOneCurrentHealth = playerOneCurrentHealth - enemyBaseDmg;
                            enemyDmgLastRound = enemyBaseDmg;
                            Console.ForegroundColor = ConsoleColor.DarkGray;//Greys out action menu for UI reasons
                            Console.WriteLine(actionBoxHor);
                        }
                        else // If enemy Choice = 2 (Critical Strike)
                        {
                            Console.WriteLine($"{playerTwoName} attempts a powerful but ungainly overhead swing.");
                            Console.WriteLine($"The creature hits for ({enemyBaseDmg} * {enemyCritMulti}) = {enemyCritDmg} points of damage!\n");
                            playerOneCurrentHealth = playerOneCurrentHealth - enemyCritDmg;
                            enemyDmgLastRound = enemyCritDmg;
                            Console.ForegroundColor = ConsoleColor.DarkGray;//Greys out action menu for UI reasons
                            Console.WriteLine(actionBoxHor);
                        }
                        for (int i = 0; i < actionArray.Length / 2; ++i)//Shows action menu
                        {
                            string actionArrayStr = ($"{actionArray[0, i]}: {actionArray[1, i]}");
                            String actionFormat = String.Format("| {0,-26} |", actionArrayStr); //{0, ZZ} ZZ = menuBoxWidth - 4          
                            Console.WriteLine(actionFormat);
                        }
                        Console.WriteLine(actionBoxHorLow);
                        Console.ResetColor();
                        Console.WriteLine("Press Enter to end turn.. ");
                        Console.ReadLine();
                        enemyAttack = false;
                    }
                    if (playerOneCurrentHealth <= 0) //Lose Condition
                    {
                        Console.Clear();
                        Console.WriteLine(headerFormat);
                        Console.WriteLine("\n");
                        Console.WriteLine($"Player {playerTwoName} Won!");
                        Console.WriteLine("\n\n");
                        Console.WriteLine(actionBoxHor);
                        Console.ForegroundColor = ConsoleColor.DarkGray; //Greys out action menu for UI reasons
                        for (int i = 0; i < actionArray.Length / 2; ++i) //Shows action menu
                        {
                            string actionArrayStr = ($"{actionArray[0, i]}: {actionArray[1, i]}");
                            String actionFormat = String.Format("| {0,-26} |", actionArrayStr); //{0, ZZ} ZZ = menuBoxWidth - 4
                            Console.WriteLine(actionFormat);
                        }
                        Console.ResetColor();
                        Console.WriteLine(actionBoxHorLow);
                        Console.WriteLine("Press Enter to Quit.. ");
                        Console.ReadLine();
                        enemyTurn = false;
                        isRunning = false;
                        //System.Environment.Exit(0); //Kan användas för att stoppa en app direkt
                    }
                    else
                    {
                        playerTurn = true;
                        enemyTurn = false;
                    }
                }
            }
        }
    }
}
