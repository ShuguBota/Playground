import random
import json

# List of Korean basic characters and their English translations
simple_characters = json.load(open('JSONFiles/kr_simple_characters.json', 'r', encoding='utf-8'))

characters_stack = list(simple_characters.keys())
random.shuffle(characters_stack)

wrong_characters_stack = []


if __name__ == "__main__":
    print("Welcome to the Korean learning program!")
    print("You will be shown a random Korean character and you will have to guess the English (Romanised) translation.")

    while(True):
        # No more characters left
        if len(characters_stack) == 0 and len(wrong_characters_stack) == 0:
            print("Congratulations! You have completed the program!")
            break

        # If the stack is empty, shuffle the wrong characters stack
        if len(characters_stack) == 0:
            random.shuffle(wrong_characters_stack)
            characters_stack = wrong_characters_stack
            wrong_characters_stack = []

        # Get a character from the stack
        character = characters_stack.pop()

        # Prompt the user to guess the English translation
        guess = input(f"What is the English translation of '{character}'? ")

        # Check if the guess is correct
        if guess.lower() == simple_characters[character]:
            print("Correct!")
        else:
            print(f"Wrong! The correct translation is '{simple_characters[character]}'")
            wrong_characters_stack.append(character)
