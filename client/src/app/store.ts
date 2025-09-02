import { configureStore } from "@reduxjs/toolkit";
import { exchangeApi } from "../api/exchangeRateApi";

export const store = configureStore({
  reducer: {
    [exchangeApi.reducerPath]: exchangeApi.reducer,
  },
  middleware: (getDefault) => getDefault().concat(exchangeApi.middleware),
});

export type RootState = ReturnType<typeof store.getState>;
export type AppDispatch = typeof store.dispatch;
