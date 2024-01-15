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


def single_close_price():
    stocks = main.read_stocks_raw('CSVStocks/NVDA_Stock_2000-2024.csv')
    stocks['Date'] = pd.to_datetime(stocks['Date'])

    # Set the 'Date' column as the index
    stocks.set_index('Date', inplace=True)

    # Plot the 'Close' column
    plt.figure(figsize=(14, 7))
    plt.plot(stocks['Close'])
    plt.title('Stock Price Over Time')
    plt.xlabel('Date')
    plt.ylabel('Close Price')
    plt.show()

def multiple_close_price():
    stocks = main.read_stocks_raw('CSVStocks/NVDA_AAPL_MSFT_Stock_2000-2024.csv')
    stocks['Date'] = pd.to_datetime(stocks['Date'])

    pivot_df = stocks.pivot_table(index='Date', columns='Ticker', values='Close')

    plt.figure(figsize=(14, 7))

    for column in pivot_df.columns:
        plt.plot(pivot_df.index, pivot_df[column], label=column)

    plt.title('Closing Pirce for NVCDA AAPL MSFT')
    plt.xlabel('Date')
    plt.ylabel('Close Price')
    plt.legend()
    plt.show()


# main.download_data(STOCK_TICKERS, '2015-01-01', '2023-11-06', 'stocks.csv')
multiple_close_price()

'''
# Risk Reward
risk_reward_plot(stocks)

# Covariance and Correlation
covariance_correlation_plot(stocks)

plt.show()
'''

# Risk & Reward potential
# Compare Covariance and Correlation
# Find 5 best to invest long term
