using System.Numerics;
using Raylib_cs;
using Sparkle.csharp;
using Sparkle.csharp.entity;
using Sparkle.csharp.entity.component;
using Sparkle.csharp.graphics;
using Sparkle.csharp.graphics.helper;
using Sparkle.csharp.gui;
using Sparkle.csharp.registry.types;
using Sparkle.csharp.scene;

namespace Test; 

public class Test3DScene : Scene {
    
    public Test3DScene(string name) : base(name) { }
    
    protected override void Init() {
        // CAMERA
        Vector3 pos = new Vector3(10.0f, 10.0f, 10.0f);
        Cam3D cam3D = new Cam3D(pos, Vector3.Zero, Vector3.UnitY, 70, CameraProjection.CAMERA_PERSPECTIVE, CameraMode.CAMERA_FREE);
        this.AddEntity(cam3D);

        for (int i = 0; i < 10; i++) {
            Entity light = new Entity(new Vector3(0, 5, 0));
            light.AddComponent(new Light(Light.LightType.Point, Vector3.Zero, Color.RED, Color.BLUE));
            this.AddEntity(light);
        }
        

        // LIGHT
        /*
        Entity light = new Entity(Vector3.Zero);
        light.AddComponent(new Light(Light.LightType.Pointed, Vector3.Zero, Color.RED));
        this.AddEntity(light);*/
        
        // TEST ENTITY
        TestEntity entity = new TestEntity(new Vector3(0, 20, 0));
        this.AddEntity(entity);

        // GROUND
        this.AddEntity(new GroundEntity(Vector3.Zero));
    }

    protected override void Update() {
        base.Update();
        
        if (Input.IsKeyPressed(KeyboardKey.KEY_E)) {
            GuiManager.SetGui(new TestGui("Test"));
        }

        if (Input.IsKeyPressed(KeyboardKey.KEY_R)) {
            GuiManager.SetGui(null);
        }
    }

    protected override void Draw() {
        base.Draw();
        
        SceneManager.MainCam3D!.BeginMode3D();
        Graphics.BeginShaderMode(ShaderRegistry.Pbr);
        
        ModelHelper.DrawGrid(100, 1);
        ModelHelper.DrawCube(SceneManager.MainCam3D.Target, 2, 2, 2, Color.RED);
        
        Graphics.EndShaderMode();
        SceneManager.MainCam3D.EndMode3D();
    }
}