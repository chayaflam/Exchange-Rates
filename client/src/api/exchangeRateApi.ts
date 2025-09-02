import { createApi, fetchBaseQuery } from "@reduxjs/toolkit/query/react";
import type { CurrencyCode, CurrenciesResponse, RatesResponse, RateRow } from "../types/exchange.types";

export const exchangeApi = createApi({
  reducerPath: "exchangeApi",
  baseQuery: fetchBaseQuery({
    baseUrl: import.meta.env.VITE_API_BASE_URL as string,
  }),
  tagTypes: ["Currencies", "Rates"],
  endpoints: (builder) => ({

    getCurrencies: builder.query<CurrenciesResponse, void>({
      query: () => "/currencies",
      providesTags: ["Currencies"],
      transformResponse: (response: CurrenciesResponse | { currencies?: CurrencyCode[] }) => {
        if (Array.isArray(response)) {
          return [...response].sort();
        }
        if (response && typeof response === "object" && "currencies" in response) {
          const arr = response.currencies ?? [];
          return Array.isArray(arr) ? [...arr].sort() : [];
        }
        return [];
      },
    }),

    getRates: builder.query<
      { base: CurrencyCode; rows: RateRow[] },
      CurrencyCode
    >({
      query: (base) => `/rates?baseCurrency=${encodeURIComponent(base)}`,
      transformResponse: (resp: RatesResponse, _meta, base) => {
        const rows = Array.isArray(resp?.rates) ? resp!.rates : [];
        return { base, rows };
      },
    })
  }),
});

export const { useGetCurrenciesQuery, useGetRatesQuery } = exchangeApi;
