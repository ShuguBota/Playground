import random
import json

class KoreanLearningProgram:
    def __init__(self):
        # List of Korean basic characters and their English translations
        self.simple_characters = json.load(open('JSONFiles/kr_simple_characters.json', 'r', encoding='utf-8'))
        self.characters_stack = list(self.simple_characters.keys())
        self.wrong_characters_stack = []

    def game_over(self):
        # No more characters left
        if len(self.characters_stack) == 0 and len(self.wrong_characters_stack) == 0:
            print("Congratulations! You have completed the program!")
            return True
        
        return False

    def get_character(self):
        # If the stack is empty, shuffle the wrong characters stack
        if len(self.characters_stack) == 0:
            random.shuffle(self.wrong_characters_stack)
            self.characters_stack = self.wrong_characters_stack
            self.wrong_characters_stack = []

        # Get a character from the stack
        return self.characters_stack.pop()

    def check_answer(self, character, guess):
        # Check if the guess is correct
        if guess.lower() == self.simple_characters[character]:
            print("Correct!")
        else:
            print(f"Wrong! The correct translation is '{self.simple_characters[character]}'")
            self.wrong_characters_stack.append(character)

    def run(self):
        print("Welcome to the Korean learning program!")
        print("You will be shown a random Korean character and you will have to guess the English (Romanised) translation.")

        random.shuffle(self.characters_stack)

        while(True):
            if self.game_over():
                break

            character = self.get_character()

            # Prompt the user to guess the English translation
            guess = input(f"What is the English translation of '{character}'? ")

            self.check_answer(character, guess)

if __name__ == "__main__":
    KoreanLearningProgram().run()