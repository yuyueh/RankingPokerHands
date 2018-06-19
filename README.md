A famous casino is suddenly faced with a sharp decline of their revenues. They decide to offer Texas hold'em also online. Can you help them by writing an algorithm that can rank poker hands?

Task:

- Create a poker hand that has a method to compare itself to another poker hand:

```
    Result PokerHand.CompareWith(PokerHand hand);
```

- A poker hand has a constructor that accepts a string containing 5 cards:

```
    PokerHand hand = new PokerHand("KS 2H 5C JD TD");
```

- The characteristics of the string of cards are:
  - A space is used as card seperator
  - Each card consists of two characters
  - The first character is the value of the card, valid characters are: 

```
2, 3, 4, 5, 6, 7, 8, 9, T(en), J(ack), Q(ueen), K(ing), A(ce)
```

- The second character represents the suit, valid characters are: 

```
S(pades), H(earts), D(iamonds), C(lubs)
```

- The result of your poker hand compare can be one of these 3 options:

```
    public enum Result 
    { 
        Win, 
        Loss, 
        Tie 
    }
```

Apply the [Texas Hold'em](https://en.wikipedia.org/wiki/Texas_hold_%27em) rules for ranking the cards.
There is no ranking for the suits.
