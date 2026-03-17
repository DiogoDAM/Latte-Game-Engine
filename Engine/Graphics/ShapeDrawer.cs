using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Latte;

public static class ShapeDrawer
{
    public static Texture2D FillRectangle(int width, int height, Color color)
    {
        Texture2D texture = new Texture2D(Engine.GraphicsDevice, width, height);
        Color[] colorData = new Color[width * height];
        
        for (int i = 0; i < colorData.Length; i++)
            colorData[i] = color;
            
        texture.SetData(colorData);
        return texture;
    }
    
    public static Texture2D LineRectangle(int width, int height, Color color, int borderWidth = 1)
    {
        Texture2D texture = new Texture2D(Engine.GraphicsDevice, width, height);
        Color[] colorData = new Color[width * height];
        
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                if (x < borderWidth || y < borderWidth || x >= width - borderWidth || y >= height - borderWidth)
                    colorData[x + y * width] = color;
                else
                    colorData[x + y * width] = Color.Transparent;
            }
        }
        
        texture.SetData(colorData);
        return texture;
    }
    
    public static Texture2D BorderedRectangle(int width, int height, Color fillColor, Color borderColor, int borderWidth = 1)
    {
        Texture2D texture = new Texture2D(Engine.GraphicsDevice, width, height);
        Color[] colorData = new Color[width * height];
        
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                if (x < borderWidth || y < borderWidth || x >= width - borderWidth || y >= height - borderWidth)
                    colorData[x + y * width] = borderColor;
                else
                    colorData[x + y * width] = fillColor;
            }
        }
        
        texture.SetData(colorData);
        return texture;
    }
    
    public static Texture2D FillCircle(int radius, Color color)
    {
        int diameter = radius * 2;
        Texture2D texture = new Texture2D(Engine.GraphicsDevice, diameter, diameter);
        Color[] colorData = new Color[diameter * diameter];
        
        Vector2 center = new Vector2(radius, radius);
        
        for (int y = 0; y < diameter; y++)
        {
            for (int x = 0; x < diameter; x++)
            {
                Vector2 position = new Vector2(x, y);
                float distance = Vector2.Distance(center, position);
                
                if (distance <= radius)
                    colorData[x + y * diameter] = color;
                else
                    colorData[x + y * diameter] = Color.Transparent;
            }
        }
        
        texture.SetData(colorData);
        return texture;
    }
    
    public static Texture2D LineCircle(int radius, Color color, int borderWidth = 1)
    {
        int diameter = radius * 2;
        Texture2D texture = new Texture2D(Engine.GraphicsDevice, diameter, diameter);
        Color[] colorData = new Color[diameter * diameter];
        
        Vector2 center = new Vector2(radius, radius);
        
        for (int y = 0; y < diameter; y++)
        {
            for (int x = 0; x < diameter; x++)
            {
                Vector2 position = new Vector2(x, y);
                float distance = Vector2.Distance(center, position);
                
                if (Math.Abs(distance - radius) <= borderWidth)
                    colorData[x + y * diameter] = color;
                else
                    colorData[x + y * diameter] = Color.Transparent;
            }
        }
        
        texture.SetData(colorData);
        return texture;
    }
    
    public static Texture2D BorderedCircle(int radius, Color fillColor, Color borderColor, int borderWidth = 1)
    {
        int diameter = radius * 2;
        Texture2D texture = new Texture2D(Engine.GraphicsDevice, diameter, diameter);
        Color[] colorData = new Color[diameter * diameter];
        
        Vector2 center = new Vector2(radius, radius);
        
        for (int y = 0; y < diameter; y++)
        {
            for (int x = 0; x < diameter; x++)
            {
                Vector2 position = new Vector2(x, y);
                float distance = Vector2.Distance(center, position);
                
                if (distance <= radius)
                {
                    if (distance >= radius - borderWidth)
                        colorData[x + y * diameter] = borderColor;
                    else
                        colorData[x + y * diameter] = fillColor;
                }
                else
                {
                    colorData[x + y * diameter] = Color.Transparent;
                }
            }
        }
        
        texture.SetData(colorData);
        return texture;
    }
    
    public static Texture2D RoundedRectangle(int width, int height, int cornerRadius, Color fillColor, Color borderColor, int borderWidth = 1)
    {
        Texture2D texture = new Texture2D(Engine.GraphicsDevice, width, height);
        Color[] colorData = new Color[width * height];
        
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                bool inside = true;
                bool isBorder = false;
                
                Vector2 cornerCenter;
                float distance;
                
                // Top-left corner
                if (x < cornerRadius && y < cornerRadius)
                {
                    cornerCenter = new Vector2(cornerRadius, cornerRadius);
                    distance = Vector2.Distance(new Vector2(x, y), cornerCenter);
                    inside = distance <= cornerRadius;
                    isBorder = inside && distance >= cornerRadius - borderWidth;
                }
                // Top-right corner
                else if (x >= width - cornerRadius && y < cornerRadius)
                {
                    cornerCenter = new Vector2(width - cornerRadius, cornerRadius);
                    distance = Vector2.Distance(new Vector2(x, y), cornerCenter);
                    inside = distance <= cornerRadius;
                    isBorder = inside && distance >= cornerRadius - borderWidth;
                }
                // Bottom-left corner
                else if (x < cornerRadius && y >= height - cornerRadius)
                {
                    cornerCenter = new Vector2(cornerRadius, height - cornerRadius);
                    distance = Vector2.Distance(new Vector2(x, y), cornerCenter);
                    inside = distance <= cornerRadius;
                    isBorder = inside && distance >= cornerRadius - borderWidth;
                }
                // Bottom-right corner
                else if (x >= width - cornerRadius && y >= height - cornerRadius)
                {
                    cornerCenter = new Vector2(width - cornerRadius, height - cornerRadius);
                    distance = Vector2.Distance(new Vector2(x, y), cornerCenter);
                    inside = distance <= cornerRadius;
                    isBorder = inside && distance >= cornerRadius - borderWidth;
                }
                // Edges
                else
                {
                    inside = true;
                    isBorder = x < borderWidth || y < borderWidth || x >= width - borderWidth || y >= height - borderWidth;
                }
                
                if (!inside)
                {
                    colorData[x + y * width] = Color.Transparent;
                }
                else if (isBorder)
                {
                    colorData[x + y * width] = borderColor;
                }
                else
                {
                    colorData[x + y * width] = fillColor;
                }
            }
        }
        
        texture.SetData(colorData);
        return texture;
    }
}
