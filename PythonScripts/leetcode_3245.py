class Solution(object):
    def duplicateZeros(self, arr):
        zeros = arr.count(0)
        for i in range(len(arr) - 1, -1, -1):
            if i + zeros < len(arr):
                arr[i + zeros] = arr[i]

            if arr[i] == 0:
                zeros -= 1
                if i + zeros < len(arr):
                    arr[i + zeros] = 0
        
        return arr  
        
sol = Solution()
print(list(sol.duplicateZeros([9,9,9,4,8,0,0,3,7,2,0,0,0,0,9,1,0,0,1,1,0,5,6,3,1,6,0,0,2,3,4,7,0,3,9,3,6,5,8,9,1,1,3,2,0,0,7,3,3,0,5,7,0,8,1,9,6,3,0,8,8,8,8,0,0,5,0,0,0,3,7,7,7,7,5,1,0,0,8,0,0])))
print(list(sol.duplicateZeros([0,1,7,6,0,2,0,7])))