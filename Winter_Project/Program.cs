using System.Collections.Generic;
using System;
using Raylib_cs;

Raylib.InitWindow(800, 600, "Game");
Raylib.SetTargetFPS(30);

const int GROUND = 580;
const int ENEMYHEIGHT = 40;
const int ENEMTWIDTH = 40;
const int PLAYERWIDTH = 50;
const int PLAYERHEIGHT = 20;
const int WEAPONWIDTH = 50;
const int WEAPONHEIGHT = 2;
const int WEAPONOFFSET = 10;
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
double enemyImmunity = 0;
double dmgTimestamp = 0;
double attackTime = 0;
double attackTimeRight = 0;
double attackTimeLeft = 0;
bool attackLeft = false;
bool attackRight = false;

Random rnd = new Random();
List<string> enemyNames = new List<string>() { "Gilbert", "Bob the builder", "Jeff", "Bosse" };
int index = rnd.Next(enemyNames.Count);
string enemyName = enemyNames[index];

Rectangle playerRect = new Rectangle(playerX, playerY, PLAYERWIDTH, PLAYERHEIGHT);
Rectangle enemy = new Rectangle(enemyX, enemyY, ENEMTWIDTH, ENEMYHEIGHT);
Rectangle playerWeapon = new Rectangle(0, 0, WEAPONWIDTH, WEAPONHEIGHT);

bool isWeaponOutRight()
{
    if (Raylib.GetTime() - attackTimeRight < 0.1)
    {


        if (attackRight)
        {
            return true;
        }
    }
    return false;
}
bool isWeaponOutLeft()
{
    if (Raylib.GetTime() - attackTimeLeft < 0.1)
    {
        if (attackLeft)
        {
            return true;
        }
    }
    return false;
}

bool playerHitbox()
{
    float playerX2 = playerX + PLAYERWIDTH;
    float playerY2 = playerY + PLAYERHEIGHT;
    float enemyX2 = enemyX + ENEMTWIDTH;
    float enemyY2 = enemyY + ENEMYHEIGHT;
    if ((enemyX >= playerX && enemyX < playerX2 && enemyY >= playerY && enemyY <= playerY2) ||
    (enemyX2 >= playerX && enemyX2 < playerX2 && enemyY2 >= playerY && enemyY2 <= playerY2)) //kollar om enemy colliderar med player för att sen returna false eller true
    {
        return true;
    }
    else
    {
        return false;
    }
}

bool enemyHitbox()
{
    float weaponX1;
    float weaponY1 = playerY + WEAPONOFFSET;
    if (attackRight)
    {
        weaponX1 = playerX + WEAPONWIDTH;
    }
    else
    {
        weaponX1 = playerX - WEAPONWIDTH;
    }
    float weaponX2 = weaponX1 + WEAPONWIDTH;
    float weaponY2 = weaponY1 + WEAPONHEIGHT;
    float enemyX2 = enemyX + ENEMTWIDTH;
    float enemyY2 = enemyY + ENEMYHEIGHT;
    if ((weaponX1 >= enemyX && weaponX1 < enemyX2 && weaponY1 >= enemyY && weaponY1 <= enemyY2) ||
    (weaponX2 >= enemyX && weaponX2 < enemyX2 && weaponY1 >= enemyY && weaponY1 <= enemyY2) ||
    (weaponX1 >= enemyX && weaponX1 < enemyX2 && weaponY2 >= enemyY && weaponY2 <= enemyY2) ||
    (weaponX2 >= enemyX && weaponX2 < enemyX2 && weaponY2 >= enemyY && weaponY2 <= enemyY2)) //kollar om fienden är i vapnets hitbox för att returna false eller true
    {
        return true;
    }
    else
    {
        return false;
    }
}

void dealDamage()
{
    if (Raylib.GetTime() - enemyImmunity > 1 || enemyImmunity == 0)
    {
        enemyHealth--;
        enemyImmunity = Raylib.GetTime();
    }
}
void takeDamage()
{
    if (Raylib.GetTime() - dmgTimestamp > 0.7 || dmgTimestamp == 0)
    {
        playerHealth--;
        dmgTimestamp = Raylib.GetTime();
    }
}

void attack(int attackDirection)
{
    if (Raylib.GetTime() - attackTime > 1 && playerY >= GROUND || attackTime == 0 && playerY >= GROUND)
    {
        if (attackDirection == 1)
        {
            attackTimeLeft = Raylib.GetTime();
            attackTime = attackTimeLeft;
            attackLeft = true;
            attackRight = false;
        }
        if (attackDirection == 2)
        {
            attackTimeRight = Raylib.GetTime();
            attackTime = attackTimeRight;
            attackRight = true;
            attackLeft = false;
        }
    }

}

void drawWeapon()
{
    if (isWeaponOutRight())
    {
        playerWeapon.y = playerY + WEAPONOFFSET;
        playerWeapon.x = playerX + PLAYERWIDTH;
        Raylib.DrawRectangleRec(playerWeapon, Color.WHITE);
    }

    if (isWeaponOutLeft())
    {
        playerWeapon.y = playerY + WEAPONOFFSET;
        playerWeapon.x = playerX - WEAPONWIDTH;
        Raylib.DrawRectangleRec(playerWeapon, Color.WHITE);
    }

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

    if (playerHitbox())
    {
        takeDamage();
    }

    if (playerHealth <= 0)
    {
        playerX = playerX + 1000000;
        enemyX = enemyX + 1000000;
        Raylib.DrawText("YOU DIED", 250, 250, 60, Color.RED);
    }
    Raylib.DrawText("Health: " + playerHealth, 40, 20, 30, Color.SKYBLUE);  //skriver ut hur mycket hp man har
    Raylib.DrawText(enemyName + ": " + enemyHealth, 500, 20, 30, Color.RED);

    if (enemyHitbox() && (isWeaponOutLeft() || isWeaponOutRight()))
    {
        dealDamage();
    }
    if (enemyHealth <= 0)
    {
        enemyX = 23948213;
        enemyY = 11101281;
        Raylib.DrawText("GREAT ENEMY FELLED", 50, 250, 60, Color.GOLD);
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
