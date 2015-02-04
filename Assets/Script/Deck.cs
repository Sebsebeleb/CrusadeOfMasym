using System.Collections.Generic;


// Represents a generic collection of cards. For example; the deck you bring to a match, the library in a match, the graveyard.
public class Deck{

    // The original cards in the deck.
    private List<ICard> originalCards;
    private List<ICard> remainingCards;

    public Deck(List<ICard> cards)
    {
        originalCards = cards;
        remainingCards = originalCards;
    }

    // Resets the deck to the state it was on at the beggining of the match (used i.e when the deck is empty and the player has to reshuffle his discard pile into a new deck)
    public void Reset(){
        remainingCards = originalCards;
        Shuffle();
    }

    public void Shuffle(){
         remainingCards.Shuffle();
    }

    // Puts new cards into the deck, then shuffles it.
    public void ShuffleInto(IEnumerable<ICard> additionalCards){
        remainingCards.AddRange(additionalCards);
        Shuffle();
    }

    public ICard DrawCard(){
        ICard draw = remainingCards[0];
        remainingCards.RemoveAt(0);

        return draw;
    }

    public int NumberOfCards(){
        return remainingCards.Count;
    }
}
