l = [0,1]

for elements in range(2, 99):
    l.append(l[elements-1]+l[elements-2])

print(l)