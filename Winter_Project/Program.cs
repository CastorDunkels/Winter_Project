using System;
using Raylib_cs;

Raylib.InitWindow(800, 600, "Game");
Raylib.SetTargetFPS(30);

Rectangle playerRect = new Rectangle(30, 30, 50, 20);
float speed = 2;

while (Raylib.WindowShouldClose() == false) {
    if (Raylib.IsKeyDown(KeyboardKey.KEY_D)) playerRect.x += speed;
    if (Raylib.IsKeyDown(KeyboardKey.KEY_A)) playerRect.x -= speed;
    if (Raylib.IsKeyDown(KeyboardKey.KEY_W)) playerRect.y -= speed;
    if (Raylib.IsKeyDown(KeyboardKey.KEY_S)) playerRect.y += speed;

    Raylib.BeginDrawing();

    Raylib.ClearBackground(Color.SKYBLUE);
    Raylib.DrawRectangleRec(playerRect, Color.GOLD);

    Raylib.EndDrawing();
}