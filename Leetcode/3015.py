from collections import deque

class Solution(object):
    def init_matrix(self, n, x, y):
        ad_matrix = [[0 for _ in range(n+1)] for _ in range(n+1)]

        for i in range(len(ad_matrix)):
            for j in range(len(ad_matrix[i])):
                if i == 0 or j == 0:
                    continue

                if i + 1 == j or i == j + 1:
                    ad_matrix[i][j] = 1
                else:
                    ad_matrix[i][j] = 0

        ad_matrix[x][y] = 1
        ad_matrix[y][x] = 1

        return ad_matrix
    
    def bfs(self, ad_matrix, start):
        q = deque()
        temp_q = deque()
        visited = [start]
        result = [0 for _ in range(len(ad_matrix))]

        q.appendleft(start)
        i = 0

        while len(q) != 0  or len(temp_q) != 0:
            if len(q) == 0:
                i += 1

                while len(temp_q) != 0:
                    q.appendleft(temp_q.popleft())
            
            else:
                current = q.popleft()
                result[current] = i
                
                for j in range(1, len(ad_matrix)):
                    if ad_matrix[current][j] == 1 and j not in visited:
                        temp_q.appendleft(j)
                        visited.append(j)

        return result

    # My long ass solution
    def countOfPairs2(self, n, x, y):
        ad_matrix = self.init_matrix(n, x, y)
        # min, i, j
        result_matrix = [[[0, 0, 0] for _ in range(n+1)] for _ in range(n+1)]

        for i in range(1, n+1):
            dist_from_i = self.bfs(ad_matrix, i)

            for j in range(1, n+1):
                if result_matrix[i][j][0] == 0 or result_matrix[i][j][0] > dist_from_i[j]:
                    result_matrix[i][j] = [dist_from_i[j], i, j]

        result = [0 for _ in range(n + 1)]

        for i in range(len(result_matrix)):
            for j in range(len(result_matrix[i])):
                result[result_matrix[i][j][0]] += 1

        return result[1:n+1]
    
    # Most efficient solution
    def countOfPairs(self, n, x, y):
        if x > y:
            return self.countOfPairs(n, y, x)
        
        result = [0 for _ in range(n)]

        for i in range(1, n + 1):
            for j in range(1, i):
                idx = i - j
                idx = min(idx, abs(j - x) + 1 + abs(i - y))

                if idx >= 1:
                    result[idx - 1] += 2

        return result
    


sol = Solution()
print(sol.countOfPairs(4, 1, 1))
