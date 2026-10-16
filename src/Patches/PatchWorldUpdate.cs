using HarmonyLib;
using PrepareLanding.Core.Gui.Window;
using RimWorld.Planet;
using UnityEngine;
using Verse;

namespace PrepareLanding.Patches
{
    [HarmonyPatch(typeof(World), "WorldUpdate")]
    class PatchWorldUpdate
    {
        [HarmonyPostfix]
        public static void WorldUpdatePostFix()
        {
            // Log.Message("[PrepareLanding] WorldUpdate postfix.");

            // don't react on the hot key while the user is typing in a text field (e.g. renaming a settlement).
            if (Input.GetKeyDown(PrepareLanding.Instance.GameOptions.PrepareLandingHotKey) && GUIUtility.keyboardControl == 0)
            {
                #if DEBUG
                Log.Message("[PrepareLanding] Shortcut key pressed.");
                #endif

                // don't add a new window if the window is already there
                if (PrepareLanding.Instance.MainWindow == null)
                    PrepareLanding.Instance.MainWindow = new MainWindow(PrepareLanding.Instance.GameData);

                PrepareLanding.Instance.MainWindow.Show();
            }

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                // make sure the prepare landing window (or its minimized window) is closed when user presses escape.
                // Only do it if it's the top most window: escape is otherwise meant for that other window (float menu, dialog...).
                var windows = Find.WindowStack.Windows;
                var topWindow = windows.Count > 0 ? windows[windows.Count - 1] : null;
                if (topWindow is MainWindow || topWindow is MinimizedWindow)
                {
                    if (PrepareLanding.Instance.MainWindow != null)
                    {
                        #if DEBUG
                        Log.Message("[PrepareLanding] Escape: Force closing.");
                        #endif

                        PrepareLanding.Instance.MainWindow.ForceClose();

                        // force clearing the filtered tile list. We still keep the filters as is.
                        PrepareLanding.Instance.TileFilter.ClearMatchingTiles();
                        PrepareLanding.Instance.TileFilter.CancelPendingLiveFilter();
                    }
                }
            }

            // live filtering: filter at most once per frame, whatever the number of user choices that changed.
            PrepareLanding.Instance.TileFilter?.ProcessPendingLiveFilter();
        }
    }
}
