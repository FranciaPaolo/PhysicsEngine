namespace Paul.PhysicsSimulator.Engine.Graphics.Camera
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Microsoft.Xna.Framework;

    /// <summary>
    /// Generic camera
    /// </summary>
    public interface ICamera
    {
        /// <summary>
        /// CameraView allow the tranformation from world-space to view-space (position of the camera)
        /// </summary>
        Matrix CameraView { get; }
        
        /// <summary>
        /// CameraProjection allow the transormation from view-space to clip-space (from 3d coordinate to 2d on the screen)
        /// </summary>
        Matrix CameraProjection { get; }                

    }
}
