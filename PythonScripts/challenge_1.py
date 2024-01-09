import main
import pandas as pd
import numpy as np
import matplotlib.pyplot as plt
import seaborn as sns

STOCK_TICKERS = [
    "AAPL", "MSFT", "ADBE", "AMZN", "TSLA", "GOOG", "JPM", "JNJ", "V", "PG",
    "NVDA", "PYPL", "UNH", "MA", "HD", "DIS", "INTC", "BAC", "NFLX", "CSCO"
]

SELECTED_TICKERS = [
    "AAPL", "MSFT", "ADBE", "AMZN", "TSLA", "GOOG", "JPM", "JNJ", "V", "PG",
    "NVDA", "PYPL", "UNH", "MA", "HD", "DIS", "INTC", "BAC", "NFLX", "CSCO"
]

# "JNJ", "PG", "UNH", "HD", "DIS", "NFLX"


def risk_reward_plot(stocks):
    stocks_mean_std = stocks['Close'][SELECTED_TICKERS].describe().T.loc[:, ['mean', 'std']]

    stocks_mean_std.plot.scatter(x='std', y='mean', figsize=(16, 9), s=50)

    for index in stocks_mean_std.index:
        plt.annotate(index, xy=(stocks_mean_std.loc[index, 'std']+0.005, stocks_mean_std.loc[index, 'mean']+0.005))
    plt.xlabel('Annual risk(std)', fontsize=15)
    plt.ylabel('Annual return', fontsize=15)
    plt.title('Risk/Return', fontsize=25)


def covariance_correlation_plot(stocks):
    close_prices = stocks['Close'][SELECTED_TICKERS]

    plt.figure(figsize=(12, 8))
    sns.set(font_scale=1.4)
    sns.heatmap(close_prices.corr(), cmap='Reds', annot=True, annot_kws={'size': 15}, vmax=0.6)


# main.download_data(STOCK_TICKERS, '2015-01-01', '2023-11-06', 'stocks.csv')

stocks = main.read_stocks('stocks.csv')

# Risk Reward
risk_reward_plot(stocks)

# Covariance and Correlation
covariance_correlation_plot(stocks)

plt.show()

# Risk & Reward potential
# Compare Covariance and Correlation
# Find 5 best to invest long term
