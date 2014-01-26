using Mangopollo;
using Mangopollo.Tiles;
using Microsoft.Phone.Shell;
using System;
using System.Diagnostics;
using System.Linq;

namespace RainyMood
{
    static class TileHelper
    {
        public static void UpdatePrimaryTile()
        {
            // only if I can use WP8 tiles - update the first tile
            if (Utils.CanUseLiveTiles)
            {
                var tileId = ShellTile.ActiveTiles.FirstOrDefault();
                if (tileId != null)
                {
                    var tileData = new FlipTileData();
                    tileData.Title = "Rainy mood";
                    tileData.BackgroundImage = new Uri("/Images/tile.medium.png", UriKind.Relative);
                    tileData.WideBackgroundImage = new Uri("/Images/tile.wide.png", UriKind.Relative);

                    
#if DEBUG
                    Debug.WriteLine("Activating live tile: " + Mangopollo.Utils.CanUseLiveTiles);
#endif
                    tileId.Update(tileData);
                }
            }
        }
    }
}
