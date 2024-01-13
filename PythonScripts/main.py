import yfinance as yf
import pandas as pd
import numpy as np
import matplotlib.pyplot as plt
import seaborn as sns

def download_data(tickers, start_date, end_date, file_name):
    data = yf.download(tickers, start=start_date, end=end_date)
    if not file_name:
        file_name = '-'.join(tickers) + '.csv'
    data.to_csv(file_name)


def read_stocks(file_path):
    # Header makes the 2 columns at the top be 1
    # Index Col deals with the Date being seen as data
    # Parse Dates forms a list of date time format
    df = pd.read_csv(file_path, header=[0, 1], index_col=[0], parse_dates=[0])

    # Make the name of the columns as tuples.
    df.columns = df.columns.to_flat_index()

    # Converting it back to multi indexes
    df.columns = pd.MultiIndex.from_tuples(df.columns)

    return df

def read_stocks_raw(file_path):
    df = pd.read_csv(file_path)

    return df


if __name__ == '__main__':
    # Initial download of the data
    '''
    tickers = ['AAPL', 'MSFT', 'GOOG']
    start_date = '2010-01-01'
    end_date = '2023-11-01'

    download_data(tickers, start_date, end_date)
    '''

    companies_csv = 'AAPL-MSFT-GOOG.csv'

    df = read_stocks(companies_csv)

    # Get only the Close columns
    close_prices = df.loc[:, 'Close'].copy()

    '''
    graph_close = close_prices.plot(figsize=(15, 8), fontsize=14)
    '''

    # Normalize (dividing the values to the first value)
    norm_close_apple = close_prices.AAPL.div(close_prices.iloc[0, 0])

    # Normalizing all the columns and multiplying them by 100
    norm_close_all = close_prices.div(close_prices.iloc[0]).mul(100)

    '''
    graph_close_norm = norm_close_all.plot(figsize=(15, 8), fontsize=14)
    '''

    # Shift function (if periods=1 then it moves the values down by 1)
    # to_frame() makes it as a new df
    close_apple = close_prices.AAPL.copy().to_frame()

    close_apple['Shift1'] = close_apple.shift(periods=1)

    # The functions are equivalent
    close_apple['Diff1'] = close_apple.AAPL.sub(close_apple.Shift1)
    close_apple['Diff1-1'] = close_apple.AAPL.diff(periods=1)

    # The functions are equivalent
    close_apple['% Change1'] = close_apple.AAPL.div(close_apple.Shift1).sub(1).mul(100)
    close_apple['% Change1-1'] = close_apple.AAPL.pct_change(periods=1).mul(100)

    # deleting data
    # del close_apple['Diff1']

    # renaming a column (if you don't make the inplace it doesn't change the name)
    close_apple.rename(columns={'Shift1': 'Shift11'}, inplace=True)

    # Gets the data of each end of the month (last business day)
    close_apple.resample('BM').last()

    # Drops the NAN variables
    close_apple.dropna()

    close_apple_1 = close_prices.AAPL.copy().to_frame().dropna()
    close_apple_1 = close_apple_1.pct_change().dropna().mul(100)
    '''
    close_apple_1.plot(kind='hist', figsize=(12, 8), bins=100)
    '''

    # Mean, Variance, Standard Variance
    daily_mean_ret = close_apple_1.mean()
    daily_var_ret = close_apple_1.var()

    # These are equivalent
    daily_std_ret = np.sqrt(daily_var_ret)
    daily_std_ret_1 = close_apple_1.std()

    # 252 working days in a year
    working_days = 252

    annual_mean_ret = daily_mean_ret * working_days
    annual_var_ret = daily_var_ret * working_days
    annual_std = np.sqrt(annual_var_ret)

    # Risk Return graph
    # In describe you get count, mean, std, min,  max, 25%, 50%, 75%
    '''
    close_description = close_prices.describe().T
    mean_std_plot = close_description.plot.scatter(x='std', y='mean', figsize=(12, 8), s=50)
    for index in close_description.index:
        plt.annotate(index, xy=(close_description.loc[index, 'std']+0.002, close_description.loc[index, 'mean']+0.002))
    plt.xlabel('Annual risk(std)', fontsize=15)
    plt.ylabel('Annual return', fontsize=15)
    plt.title('Risk/Return', fontsize=25)
    '''

    # Correlation and Covariance
    # You would want to pick companies with less correlation between them
    ''''''
    plt.figure(figsize=(12, 8))
    sns.set(font_scale=1.4)
    sns.heatmap(close_prices.corr(), cmap='Reds', annot=True, annot_kws={'size': 15}, vmax=0.6)

    plt.show()
