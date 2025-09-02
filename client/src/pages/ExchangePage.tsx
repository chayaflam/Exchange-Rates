import React from "react";
import { useGetCurrenciesQuery, useGetRatesQuery } from "../api/exchangeRateApi";
import { CurrencyTable } from "../components/CurrencyTable";
import type { CurrencyCode } from "../types/exchange.types";
export default function ExchangePage() {
  const [base, setBase] = React.useState<CurrencyCode>("USD");

  const { data: currencies, isLoading: loadingCurrencies } = useGetCurrenciesQuery();
  const { data: rates, isLoading: loadingRates, isFetching, refetch, isError } = useGetRatesQuery(base, {
    refetchOnFocus: true,
  });

  if (loadingCurrencies || loadingRates) return <div>Loading…</div>;
  if (isError || !currencies || !rates) return <div>Error <button onClick={() => refetch()}>Retry</button></div>;




  return (
    <div className="flex items-center flex-col">
      <h2 className="font-bold text-lg flex item-center">Exchange Rates</h2>
      <div className="border-gray-200 bg-white p-4 dark:border-gray-800 dark:bg-gray-900">


        <label className="p-10 text-sm font-medium text-gray-700 dark:text-gray-200">Base currency: </label>
        <select value={base}
          onChange={(e) => setBase(e.target.value as CurrencyCode)} className="min-w-[8rem] rounded-md border border-gray-300 bg-white px-3 py-2 text-sm shadow-sm
             focus:outline-none focus:ring-2 focus:ring-indigo-500
             dark:border-gray-700 dark:bg-gray-900 dark:text-gray-100" >
          {currencies.map((c) => (
            <option key={c} value={c}>{c}</option>
          ))}
        </select>

        {isFetching && (
          <small className="ml-2 text-xs text-gray-500 dark:text-gray-400">Updating…</small>
        )}
      </div>
      <div className="rounded-xl border border-gray-200 bg-white shadow-sm dark:border-gray-800 dark:bg-gray-900">
        <CurrencyTable
          data={rates.rows.map((r) => ({
            base: rates.base,
            currency: r.currency,
            rate: r.rate.toString() // נשאר בדיוק כמו בקוד שלך
          }))}
        />
      </div>
    </div>
  );
}
