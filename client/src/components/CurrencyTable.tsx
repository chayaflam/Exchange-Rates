import React from "react";
import {
  createColumnHelper,
  flexRender,
  getCoreRowModel,
  getSortedRowModel,
  type SortingState,
  useReactTable,
} from "@tanstack/react-table";

import type { ExchangeRow } from "../types/exchange.types";

const columnHelper = createColumnHelper<ExchangeRow>();

const columns = [
  columnHelper.accessor("base", {
    header: "Base",
    cell: (info) => String(info.getValue() ?? ""),
  }),
  columnHelper.accessor("currency", {
    header: "Currency",
    cell: (info) => String(info.getValue() ?? ""),
  }),
  columnHelper.accessor("rate", {
    header: "Rate",
    cell: (info) => String(info.getValue()),

  }),
];

export function CurrencyTable({ data }: { data: ExchangeRow[] }) {
  const [sorting, setSorting] = React.useState<SortingState>([]);

  const table = useReactTable({
    data,
    columns,
    state: { sorting },
    onSortingChange: setSorting,
    getCoreRowModel: getCoreRowModel(),
    getSortedRowModel: getSortedRowModel(),
  });

  return (
    <table className="p-10 min-w-full border-collapse text-sm text-left">
      <thead className="bg-gray-50 dark:bg-gray-800/60">
        {table.getHeaderGroups().map((hg) => (
          <tr key={hg.id}>
            {hg.headers.map((header) => {
              const sorted = header.column.getIsSorted();
              return (
                <th
                  key={header.id}
                  onClick={header.column.getToggleSortingHandler()}
                  className="px-3 py-2 border border-gray-300 text-gray-700 dark:text-gray-200 cursor-pointer select-none text-left"
                  title="Click to sort"
                >
                  {flexRender(header.column.columnDef.header, header.getContext())}
                  {" "}
                  {sorted === "asc" ? "▲" : sorted === "desc" ? "▼" : ""}
                </th>
              );
            })}
          </tr>
        ))}
      </thead>
      <tbody className="divide-y divide-gray-100 dark:divide-gray-800">
        {table.getRowModel().rows.map((row) => (
          <tr key={row.id}>
            {row.getVisibleCells().map((cell) => (
             <td key={cell.id} className="px-3 py-2 border border-gray-200 text-gray-700 dark:text-gray-300">
                {flexRender(cell.column.columnDef.cell, cell.getContext())}
              </td>
            ))}
          </tr>
        ))}
      </tbody>
    </table>
  );
}