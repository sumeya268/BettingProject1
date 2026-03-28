using System;
using System.Collections.Generic;
using sports_betting.Models;
using sports_betting.Services;

class Program
{
    // These are the backend services that handle all the logic
    // for bets and each sport. Program.cs only controls the UI.
    static BetService betService = new BetService();
    static FootballService footballService = new FootballService();
    static BasketballService basketballService = new BasketballService();
    static AmericanFootballService americanFootballService = new AmericanFootballService();
    static HorseRacingService horseRacingService = new HorseRacingService();
    static F1Service f1Service = new F1Service();

    static void Main(string[] args)
    {
        // Main loop that keeps the program running until the user exits
        bool running = true;

        while (running)
        {
            Console.Clear();
            Console.WriteLine("=== BETTING GAME BACKEND ===");
            Console.WriteLine("1. Football");
            Console.WriteLine("2. Basketball");
            Console.WriteLine("3. American Football");
            Console.WriteLine("4. Horse Racing");
            Console.WriteLine("5. F1 Racing");
            Console.WriteLine("6. View All Bets");
            Console.WriteLine("7. Search Bet");
            Console.WriteLine("8. Delete Bet");
            Console.WriteLine("9. Exit");

            // Input validation helper ensures user enters a number
            int choice = GetValidInt("Choose an option: ");

            // Menu navigation
            switch (choice)
            {
                case 1: FootballMenu(); break;
                case 2: BasketballMenu(); break;
                case 3: AmericanFootballMenu(); break;
                case 4: HorseRacingMenu(); break;
                case 5: F1Menu(); break;
                case 6: ShowAllBets(); break;
                case 7: SearchBet(); break;
                case 8: DeleteBet(); break;
                case 9: running = false; break;
                default:
                    Console.WriteLine("Invalid choice. Press any key to continue.");
                    Console.ReadKey();
                    break;
            }
        }
    }

    // ---------------------- SPORT MENUS ----------------------
    // Each sport menu allows the user to view events or place a bet.

    static void FootballMenu()
    {
        Console.Clear();
        Console.WriteLine("=== FOOTBALL MENU ===");
        Console.WriteLine("1. View Events");
        Console.WriteLine("2. Place Bet");
        Console.WriteLine("3. Back");

        int choice = GetValidInt("Choose an option: ");

        switch (choice)
        {
            case 1: ShowEvents(footballService.GetEvents()); break;
            case 2: PlaceBet("Football", footballService.GetEvents()); break;
            case 3: return; // Go back to main menu
            default:
                Console.WriteLine("Invalid choice. Press any key to continue.");
                Console.ReadKey();
                break;
        }
    }

    static void BasketballMenu()
    {
        Console.Clear();
        Console.WriteLine("=== BASKETBALL MENU ===");
        Console.WriteLine("1. View Events");
        Console.WriteLine("2. Place Bet");
        Console.WriteLine("3. Back");

        int choice = GetValidInt("Choose an option: ");

        switch (choice)
        {
            case 1: ShowEvents(basketballService.GetEvents()); break;
            case 2: PlaceBet("Basketball", basketballService.GetEvents()); break;
            case 3: return;
            default:
                Console.WriteLine("Invalid choice. Press any key to continue.");
                Console.ReadKey();
                break;
        }
    }

    static void AmericanFootballMenu()
    {
        Console.Clear();
        Console.WriteLine("=== AMERICAN FOOTBALL MENU ===");
        Console.WriteLine("1. View Events");
        Console.WriteLine("2. Place Bet");
        Console.WriteLine("3. Back");

        int choice = GetValidInt("Choose an option: ");

        switch (choice)
        {
            case 1: ShowEvents(americanFootballService.GetEvents()); break;
            case 2: PlaceBet("American Football", americanFootballService.GetEvents()); break;
            case 3: return;
            default:
                Console.WriteLine("Invalid choice. Press any key to continue.");
                Console.ReadKey();
                break;
        }
    }

    static void HorseRacingMenu()
    {
        Console.Clear();
        Console.WriteLine("=== HORSE RACING MENU ===");
        Console.WriteLine("1. View Events");
        Console.WriteLine("2. Place Bet");
        Console.WriteLine("3. Back");

        int choice = GetValidInt("Choose an option: ");

        switch (choice)
        {
            case 1: ShowEvents(horseRacingService.GetEvents()); break;
            case 2: PlaceBet("Horse Racing", horseRacingService.GetEvents()); break;
            case 3: return;
            default:
                Console.WriteLine("Invalid choice. Press any key to continue.");
                Console.ReadKey();
                break;
        }
    }

    static void F1Menu()
    {
        Console.Clear();
        Console.WriteLine("=== F1 RACING MENU ===");
        Console.WriteLine("1. View Events");
        Console.WriteLine("2. Place Bet");
        Console.WriteLine("3. Back");

        int choice = GetValidInt("Choose an option: ");

        switch (choice)
        {
            case 1: ShowEvents(f1Service.GetEvents()); break;
            case 2: PlaceBet("F1 Racing", f1Service.GetEvents()); break;
            case 3: return;
            default:
                Console.WriteLine("Invalid choice. Press any key to continue.");
                Console.ReadKey();
                break;
        }
    }

    // ---------------------- EVENT DISPLAY ----------------------
    // Shows all available events for a sport.

    static void ShowEvents(List<Event> events)
    {
        Console.Clear();
        Console.WriteLine("=== EVENTS ===");

        if (events == null || events.Count == 0)
        {
            Console.WriteLine("No events available.");
            Console.WriteLine("\nPress any key to return...");
            Console.ReadKey();
            return;
        }

        int index = 1;
        foreach (var e in events)
        {
            Console.WriteLine($"{index}. {e.EventName} | {e.Selection1}: {e.Odds1} | {e.Selection2}: {e.Odds2}");
            index++;
        }

        Console.WriteLine("\nPress any key to return...");
        Console.ReadKey();
    }

    // ---------------------- PLACE BET ----------------------
    // Handles the full bet placement process:
    // choosing event → choosing selection → entering stake → saving bet.

    static void PlaceBet(string sport, List<Event> events)
    {
        Console.Clear();
        Console.WriteLine($"=== PLACE BET ({sport}) ===");

        if (events == null || events.Count == 0)
        {
            Console.WriteLine("No events to bet on.");
            Console.ReadKey();
            return;
        }

        // Show event list
        for (int i = 0; i < events.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {events[i].EventName}");
        }

        int eventChoice = GetValidInt("Choose an event: ") - 1;

        if (eventChoice < 0 || eventChoice >= events.Count)
        {
            Console.WriteLine("Invalid event.");
            Console.ReadKey();
            return;
        }

        var selectedEvent = events[eventChoice];

        // Show selections
        Console.WriteLine($"1. {selectedEvent.Selection1} ({selectedEvent.Odds1})");
        Console.WriteLine($"2. {selectedEvent.Selection2} ({selectedEvent.Odds2})");

        int selectionChoice = GetValidInt("Choose a selection: ");

        string selection;
        double odds;

        if (selectionChoice == 1)
        {
            selection = selectedEvent.Selection1;
            odds = selectedEvent.Odds1;
        }
        else if (selectionChoice == 2)
        {
            selection = selectedEvent.Selection2;
            odds = selectedEvent.Odds2;
        }
        else
        {
            Console.WriteLine("Invalid selection.");
            Console.ReadKey();
            return;
        }

        double stake = GetValidDouble("Enter stake amount: ");

        // Create and save the bet
        Bet bet = new Bet
        {
            BetID = betService.GenerateBetID(),
            Sport = sport,
            EventName = selectedEvent.EventName,
            Selection = selection,
            Odds = odds,
            Stake = stake,
            PotentialReturn = stake * odds
        };

        betService.AddBet(bet);

        Console.WriteLine($"Bet placed! Bet ID: {bet.BetID}");
        Console.ReadKey();
    }

    // ---------------------- BET MANAGEMENT ----------------------
    // View all bets, search for a bet, or delete a bet.

    static void ShowAllBets()
    {
        Console.Clear();
        var bets = betService.GetAllBets();

        if (bets == null || bets.Count == 0)
        {
            Console.WriteLine("No bets found.");
        }
        else
        {
            foreach (var bet in bets)
            {
                Console.WriteLine($"ID: {bet.BetID} | {bet.Sport} | {bet.EventName} | {bet.Selection} | Stake: {bet.Stake} | Return: {bet.PotentialReturn}");
            }
        }

        Console.ReadKey();
    }

    static void SearchBet()
    {
        Console.Clear();
        int id = GetValidInt("Enter Bet ID: ");

        var bet = betService.SearchBet(id);

        if (bet == null)
        {
            Console.WriteLine("Bet not found.");
        }
        else
        {
            Console.WriteLine($"Found Bet: {bet.EventName} | {bet.Selection} | Stake: {bet.Stake} | Return: {bet.PotentialReturn}");
        }

        Console.ReadKey();
    }

    static void DeleteBet()
    {
        Console.Clear();
        int id = GetValidInt("Enter Bet ID to delete: ");

        bool deleted = betService.DeleteBet(id);

        Console.WriteLine(deleted ? "Bet deleted." : "Bet not found.");
        Console.ReadKey();
    }

    // ---------------------- INPUT VALIDATION HELPERS ----------------------
    // These ensure the user enters valid numbers.

    static int GetValidInt(string message)
    {
        int value;
        Console.Write(message);

        while (!int.TryParse(Console.ReadLine(), out value))
        {
            Console.Write("Invalid input. Please enter a number: ");
        }

        return value;
    }

    static double GetValidDouble(string message)
    {
        double value;
        Console.Write(message);

        while (!double.TryParse(Console.ReadLine(), out value))
        {
            Console.Write("Invalid input. Please enter a valid amount: ");
        }

        return value;
    }
}
