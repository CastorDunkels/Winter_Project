using System.ComponentModel.DataAnnotations;
using System;
using Raylib_cs;

Raylib.InitWindow(800, 600, "Game");
Raylib.SetTargetFPS(30);

const int GROUND = 580;
float playerHealth = 3;
float playerX = 100;
float playerY = GROUND;
float speed = 6;
float WalkingSpeed = 0;
float FlyingSpeed = 0;
float Gravity = 0;
float Jump = -10;
float Acceleration = 0.6f;
float Direction = 0;
float leftWall = 0;
float rightWall = 750;
float enemyX = 500;
float enemyY = GROUND - 20;
float enemyHealth = 5;
double dmgTimestamp = 0;
double attackTime = 0;
bool attackLeft = false;
bool attackRight = false;

Rectangle playerRect = new Rectangle(playerX, playerY, 50, 20);
Rectangle enemy = new Rectangle(enemyX, enemyY, 40, 40);
Rectangle playerWeapon = new Rectangle(0, 0, 100, 2);

void takeDamage()
{
    if (Raylib.GetTime() - dmgTimestamp > 0.5 || dmgTimestamp == 0)
    {
        playerHealth--;
        dmgTimestamp = Raylib.GetTime();
    }
}

void attack(int attackDirection)
{
    attackTime = Raylib.GetTime();
    if (attackDirection == 1)
    {
        attackLeft = true;
    }
    if (attackDirection == 2)
    {
        attackRight = true;
    }
}
void drawWeapon()
{
    //if (Raylib.GetTime() - attackTime > 1 || attackTime == 0)
    //{

    if (attackLeft)
    {
        playerWeapon.y = playerY + 10;
        playerWeapon.x = playerX - 100;
        Raylib.DrawRectangleRec(playerWeapon, Color.WHITE);
    }
    if (attackRight)
    {
        playerWeapon.y = playerY + 10;
        playerWeapon.x = playerX + 50;
        Raylib.DrawRectangleRec(playerWeapon, Color.WHITE);
    }
    //}
}

while (Raylib.WindowShouldClose() == false)
{
    if (playerX < enemyX)
    {
        enemyX -= speed / 2;  //gör så att fienden går mot spelaren beroende på om man har högre x värde eller mindre x värde
    }
    if (playerX > enemyX)
    {
        enemyX += speed / 2;
    }


    WalkingSpeed = 0;
    if (Raylib.IsKeyDown(KeyboardKey.KEY_D))
    {
        Direction = 1;
        if (GROUND - playerY < 1)
        { //detta kollar om man är på marken eller inte så man inte kan fortsätta gå runt när man är i luften
            playerX += speed;

            WalkingSpeed = speed * Direction;

        }

    }
    if (Raylib.IsKeyDown(KeyboardKey.KEY_A))  //göra en metod för att gå kanske
    {
        Direction = -1;
        if (GROUND - playerY < 1)
        {
            playerX -= speed;

            WalkingSpeed = speed * Direction;

        }

    }


    if (Raylib.IsKeyDown(KeyboardKey.KEY_SPACE))
        if (GROUND - playerY < 1)
        {   //detta gör så att man inte kan hoppa mer än när man är på marken
            Gravity = Jump; //detta gör så att när man trycker på space så blir gravitationen minus så att man går uppot som är som att hoppa
            FlyingSpeed = WalkingSpeed;
        }



    if (playerY - GROUND < -20)
    {
        playerX += FlyingSpeed; //detta gör så att man håller kvar sin speed när man hoppar
    }

    playerY += Gravity;
    if (playerY > GROUND)  //detta gör så att gubben inte kan åka under marken eller gå ut åt sidorna
    {
        playerY = GROUND;
    }
    if (playerX < leftWall + 5)
    {
        playerX = leftWall;
    }
    if (playerX > rightWall)
    {
        playerX = rightWall;
    }

    if (enemyX < playerX + 45 && enemyX > playerX - 45 && enemyY <= playerY + 25) //kollar om enemy colliderar med player för att se om hp ska minskas
    {
        takeDamage();
    }
    if (playerHealth <= 0)
    {
        playerX = playerX + 1000000;
        enemyX = enemyX + 1000000;
        Raylib.DrawText("YOU DIED", 250, 250, 60, Color.RED);
    }
    Raylib.DrawText("Health: " + playerHealth, 40, 20, 30, Color.RED);  //skriver ut hur mycket hp man har

    //if(enemyX = playerWeapon.x)
    if (enemyHealth <= 0)
    {

    }

    if (playerY == GROUND)
    {
        Gravity = 0;
    }
    else
    {
        Gravity += Acceleration;  //detta gör så att när man är över marken så går gravity igång 
    }

    if (Raylib.IsKeyDown(KeyboardKey.KEY_RIGHT))
    {

        attack(2);
    }
    if (Raylib.IsKeyDown(KeyboardKey.KEY_LEFT))
    {

        attack(1);
    }

    playerRect.y = playerY;  //detta gör så att visuella delen och modellen är separata
    playerRect.x = playerX;
    enemy.x = enemyX;
    enemy.y = enemyY;

    Raylib.BeginDrawing();
    drawWeapon();
    Raylib.ClearBackground(Color.BLACK);
    Raylib.DrawRectangleRec(playerRect, Color.SKYBLUE);
    Raylib.DrawRectangleRec(enemy, Color.RED);

    Raylib.EndDrawing();
}
