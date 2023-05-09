using System;
using System.Runtime.CompilerServices;

namespace Battleship
{
    public class SuperCoolAgent : BattleshipAgent
    {
        char[,] attackHistory;
        GridSquare attackGrid;
        Random rng;        
        GridSquare last;
        bool startMode;
        int sweepDirection;

        

        public SuperCoolAgent()
        {
            Nickname = "b ryce"; //name bot
            attackHistory = new char[10, 10];
            //attackGrid = new GridSquare();
            rng = new Random();

        }

        public override void Initialize() //resets the board after each game
        {
            attackHistory = new char[10, 10];
            attackGrid = new GridSquare();            
                        
            last = new GridSquare();
            startMode = true; //I switched from three modes to two modes, but I use an else to go into sweep Mode
            sweepDirection = 0; //this was easier than the array, as I kind of made the code a loop

            return;
        }

        public override string ToString()
        {
            return $"Battleship Agent '{Nickname}'";
        }

        public override void SetOpponent(string opponent)
        {
            return;
        }
           
           
        
        
        public override GridSquare LaunchAttack()
        {


            if (startMode == true)
            {
                while (attackHistory[attackGrid.x, attackGrid.y] != '\0') //fires randomly until hit
                {
                    attackGrid.x = rng.Next(0, 10);
                    attackGrid.y = rng.Next(0, 10);
                }
            }
            
            else //executed when there is a hit
            {

                if (sweepDirection == 1 && last.y - 1 >= 0 && attackHistory[last.x, last.y - 1] == '\0')
                {
                    attackGrid.x = last.x;
                    attackGrid.y = last.y - 1;

                    //if (sweepDirection == 1 && attackGrid.y - 1 == 0 && attackHistory[last.x, last.y - 1] != '\0')
                    //{
                    //    startMode = true; 
                    //}
                }
                else if (sweepDirection == 2 && last.y + 1 <= 9 && attackHistory[last.x, last.y + 1] == '\0')
                {
                    attackGrid.x = last.x;
                    attackGrid.y = last.y + 1;

                    //if (sweepDirection == 2 && attackGrid.y + 1 == 10 && attackHistory[last.x, last.y + 1] != '\0')
                    //{
                    //    startMode = true;
                    //}

                }
                else if (sweepDirection == 3 && last.x + 1 <= 9 && attackHistory[last.x + 1, last.y] == '\0')
                {
                    attackGrid.x = last.x + 1;
                    attackGrid.y = last.y;

                    //if (sweepDirection == 3 && attackGrid.x + 1 == 10 && attackHistory[last.x + 1, last.y] != '\0')
                    //{
                    //    startMode = true;
                    //}


                }
                else if (sweepDirection == 4 && last.x - 1 >= 0 && attackHistory[last.x - 1, last.y] == '\0')
                {
                    attackGrid.x = last.x - 1;
                    attackGrid.y = last.y;

                    //if (sweepDirection == 4 && attackGrid.x - 1 == 0 && attackHistory[last.x - 1, last.y] != '\0')
                    //{
                    //    startMode = true;
                    //}

                }
                
                else
                {
                    attackGrid.x = rng.Next(10);
                    attackGrid.y = rng.Next(10);

                }
                sweepDirection++; //Increment the loop

                if (sweepDirection > 4) // We do not want sweepDirection > 4, I tried to prevent this with a mod, but sweepDirection = 0 messes it up 
                {
                    sweepDirection = 0;
                    startMode = true;
                }
            }

            return attackGrid;




        }
            

            
            
    






        public override void DamageReport(char report)
        {
            
                if (report == '\0')
                {
                    attackHistory[attackGrid.x, attackGrid.y] = 'M'; //miss
                    
                }
            else
            {
                if (startMode == true)
                {
                    startMode = false; //sweep mode
                    sweepDirection = 1;
                    last.x = attackGrid.x;
                    last.y = attackGrid.y;
                }
                attackHistory[attackGrid.x, attackGrid.y] = report;
            }





        }
            

        

        public override BattleshipFleet PositionFleet()
        {
            BattleshipFleet myFleet = new BattleshipFleet();

            //randomly generates ships in different parts of the board

            myFleet.Carrier = new ShipPosition(rng.Next(1,4), 0, ShipRotation.Vertical);
            myFleet.Battleship = new ShipPosition(6, rng.Next(0,4), ShipRotation.Horizontal);
            myFleet.Destroyer = new ShipPosition(rng.Next(0, 4), rng.Next(5, 8), ShipRotation.Vertical);
            myFleet.Submarine = new ShipPosition(4, rng.Next(6, 10), ShipRotation.Horizontal);
            myFleet.PatrolBoat = new ShipPosition(8, rng.Next(6, 10), ShipRotation.Horizontal);

            return myFleet;
        }
    }
}