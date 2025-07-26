using System;

namespace GameEventDispatcher
{
    // 1. Define the delegate
    public delegate void GameEventHandler();

    // 2. Game class aggregates all listeners
    public class Game
    {
        // Multicast delegate field
        public GameEventHandler OnLevelComplete;

        public void CompleteLevel()
        {
            Console.WriteLine("🏁 Level Completed!");
            
            // Invoke all attached listeners
            OnLevelComplete?.Invoke();
        }
    }

    // 3. Listener classes
    public class SaveSystem
    {
        public void SaveGame()
        {
            Console.WriteLine("💾 Game saved.");
        }
    }

    public class UIManager
    {
        public void UpdateUI()
        {
            Console.WriteLine("🖥️ UI updated.");
        }
    }

    public class AnalyticsTracker
    {
        public void TrackLevelCompletion()
        {
            Console.WriteLine("📊 Analytics updated.");
        }
    }

    // 4. Program to tie everything
    class Program
    {
        static void Main(string[] args)
        {
            // Create the game and systems
            Game game = new Game();
            SaveSystem saveSystem = new SaveSystem();
            UIManager uiManager = new UIManager();
            AnalyticsTracker analytics = new AnalyticsTracker();

            // Attach event handlers (delegates)
            game.OnLevelComplete += saveSystem.SaveGame;
            game.OnLevelComplete += uiManager.UpdateUI;
            game.OnLevelComplete += analytics.TrackLevelCompletion;

            // Complete a level
            game.CompleteLevel();

        }
    }
}
