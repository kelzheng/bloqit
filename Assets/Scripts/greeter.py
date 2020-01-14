import random
import numpy as np
class Greeter():
    def __init__(self, name):
        self.name = name
    def greet(self):
    	a = np.matrix('1 2; 3 4')
    	b = np.matrix('1 2; 3 4')
    	c = np.matmul(a, b)
        return "Hi, " + self.name + ", here is a matrix : \n", c, "\n\n"
    def random_number(self, start, end):
    	return random.randint(start, end)