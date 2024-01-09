import numpy as np
import pandas as pd
import matplotlib.pyplot as plt
import main

from sklearn.preprocessing import PolynomialFeatures
from sklearn.linear_model import LinearRegression

stocks = main.read_stocks('stocks.csv')

apple_close = stocks['Close'].AAPL

X = apple_close.index.astype('int64').values.reshape(-1, 1)
y = apple_close.values

poly = PolynomialFeatures(degree=2)
poly.fit_transform(X, y)

print(poly)
