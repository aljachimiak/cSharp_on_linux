using System;
using System.Collections.Generic;
using System.Linq;

public class Cards
{
	const string SEP1 = "========================================";
	const string SEP2 = "----------------------------------------";
	
	public class Card
	{
		public string Suit { get; set; }
		public int Value { get; set; }
	}
	
	static public List<Card> NewDeck()
	{
		List<Card> deck = new List<Card>();
		List<string> suits = new List<string> { "♥", "♦", "♣", "♠" };
		List<int> values = new List<int> { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
		
		foreach (var s in suits)
		{
			foreach ( var v in values)
			{
				Card card = new Card { Suit = s, Value = v};
				deck.Add(card);
			}
		}
		return deck;
	}
	
	static public void PrintDeck( List<Card> deck)
	{	
		int oldLine = 0;
		Console.WriteLine(System.Environment.NewLine);
		
		for (int i = 0; i < deck.Count; i++)
		{
			int newLine = (int)Math.Floor( (double)i/10 );
			if (newLine > oldLine)
			{
				oldLine = newLine;
				Console.WriteLine(System.Environment.NewLine);
			}
			Card temp = deck.ElementAt(i);
			Console.Write(temp.Suit + temp.Value + "  ");
		}
		Console.WriteLine(System.Environment.NewLine);
	}
	
	static private List<Card> Shuffle( List<Card> deck)
	{
		List<Card> shuffled = new List<Card>();
		
		Random ran = new Random();
		while (deck.Count > 0)
		{
			int cardIndex = ran.Next(deck.Count);
			Card card = deck.ElementAt(cardIndex);
			shuffled.Add(card);
			deck.RemoveAt(cardIndex);
		}
		return shuffled;
	}
	
	static public void AwaitInput(List<Card> deck)
	{
		Console.WriteLine(SEP2);
		Console.WriteLine("Choose an option:");
		Console.WriteLine("1 - Print the deck again");
		Console.WriteLine("2 - Shuffle the deck");
		Console.WriteLine("3 - Sort the deck by Suit");
		Console.WriteLine("4 - Sort by Value");
		Console.WriteLine("5 - Sort by Suit and Value");
		Console.WriteLine("q - Quit");
		Console.WriteLine(System.Environment.NewLine);
		Console.Write("--> ");
		
		string s = Console.ReadLine();
		switch (s)
		{
			case "1":
				PrintDeck(deck);
				AwaitInput(deck);
				break;
			case "2":
				var shuffled = Shuffle(deck);
				PrintDeck(shuffled);
				AwaitInput(shuffled);
				break;
			case "3":
				var sortedS = deck.OrderBy( c => c.Suit ).ToList();
				PrintDeck(sortedS);
				AwaitInput(sortedS);
				break;
			case "4":
				var sortedV = deck.OrderBy( c => c.Value ).ToList();
				PrintDeck(sortedV);
				AwaitInput(sortedV);
				break;
			case "5":
				var sortedSV = deck.OrderBy( c => c.Suit ).ThenBy( d => d.Value ).ToList();
				PrintDeck(sortedSV);
				AwaitInput(sortedSV);
				break;
			case "q":
				Console.WriteLine(System.Environment.NewLine);
				Console.WriteLine("----------------- Bye ------------------");
				Console.WriteLine(System.Environment.NewLine);
				Console.WriteLine(SEP1);
				Console.WriteLine(System.Environment.NewLine);
				break;
			default:
				AwaitInput(deck);
				break;
		}
	} 
	
	static public void Main()
	{
		Console.Clear();
		
		Console.WriteLine(SEP1);
		Console.WriteLine("Here is a Deck of Cards:");
		List<Card> deck = NewDeck();
		PrintDeck(deck);
		AwaitInput(deck);
	}
}