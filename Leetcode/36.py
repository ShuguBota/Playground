class Solution(object):
    def checkLine(self, line):
        number_set = set()
        
        for l in line:
            if l != '.' and l in number_set:
                return False
            
            number_set.add(l)

    def isValidSudoku(self, board):
        # Line Check
        for i in range(0, len(board)):
            if self.checkLine(board[i]) == False:
                return False
            
        # Column Check
        for i in range(0, len(board)):
            number_set = set()

            for j in range(0, len(board[i])):
                if board[j][i] != '.' and board[j][i] in number_set:
                    return False
                
                number_set.add(board[j][i])

        # Square check
        for indexi in range(0, 3):
            for indexj in range(0, 3):
                number_set = set()

                for i in range(0, 3):
                    for j in range(0, 3):
                        if board[indexi * 3 + i][indexj * 3 + j] != '.' and board[indexi * 3 + i][indexj * 3 + j] in number_set:
                            return False
                    
                        number_set.add(board[indexi * 3 + i][indexj * 3 + j])

        return True