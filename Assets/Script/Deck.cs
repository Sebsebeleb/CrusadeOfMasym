using System.Collections.Generic;

// Represents a generic collection of cards. For example; the deck you bring to a match, the library in a match, the graveyard.
public class Deck
{
    // The original cards in the deck.
    private List<BaseCard> originalCards;
    private List<BaseCard> remainingCards;

    public Deck(List<BaseCard> cards)
    {
        originalCards = cards;
        remainingCards = originalCards;
    }

    // Resets the deck to the state it was on at the beggining of the match (used i.e when the deck is empty and the player has to reshuffle his discard pile into a new deck)
    public void Reset()
    {
        remainingCards = originalCards;
        Shuffle();
    }

    public void Shuffle()
    {
        remainingCards.Shuffle();
    }

    // Puts new cards into the deck, then shuffles it.
    public void ShuffleInto(IEnumerable<BaseCard> additionalCards)
    {
        remainingCards.AddRange(additionalCards);
        Shuffle();
    }

    public BaseCard DrawCard()
    {
        //If the library is empty, we shuffle it before drawing
        if (remainingCards.Count <= 0) {
            Reset();
        }

        BaseCard draw = remainingCards[0];
        remainingCards.RemoveAt(0);

        return draw;
    }

    public int NumberOfCards()
    {
        return remainingCards.Count;
    }
}