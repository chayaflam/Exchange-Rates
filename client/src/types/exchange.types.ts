export type CurrencyCode = "USD" | "EUR" | "GBP" | "CNY" | "ILS";

export type CurrenciesResponse = CurrencyCode[];

export type RatesResponse = {
  base?: string;
  rates?: Record<string, string>;
};

export type RateRow = {
  currency: string;
  rate: number;
};


export type ExchangeRow = {
  base: string;
  currency: string;
  rate: string;
};