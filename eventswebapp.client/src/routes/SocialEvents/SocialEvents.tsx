import { Fragment, useEffect, useState } from 'react';
import { PageParams, SocialEventModel } from '../../types/types';
import axios from 'axios';
import httpAgent from '../../utils/httpAgent';
import { Box, Paper } from '@mui/material';
import {
  MaterialReactTable,
  MRT_ColumnFiltersState,
  MRT_PaginationState,
  MRT_SortingState,
} from 'material-react-table';

interface Props {}
export default function SocialEvents(props: Props) {
  const [tableData, setTableData] = useState<SocialEventModel[]>([]);
  const [userParams, setParams] = useState<PageParams>({
    orderBy: '',
    pageNumber: 1,
    pageSize: 10,
    searchTerm: '',
    columnFilters: '',
  });
  const [isError, setIsError] = useState(false);
  const [isLoading, setIsLoading] = useState(false);
  const [isRefetching, setIsRefetching] = useState(false);
  const [rowCount, setRowCount] = useState(0);
  const [columnFilters, setColumnFilters] = useState<MRT_ColumnFiltersState>(
    []
  );

  const [globalFilter, setGlobalFilter] = useState('');

  const [sorting, setSorting] = useState<MRT_SortingState>([]);

  const [pagination, setPagination] = useState<MRT_PaginationState>({
    pageIndex: 0,
    pageSize: 10,
  });

  function getAxiosParams(pageParams: PageParams) {
    const params = new URLSearchParams();
    params.append('pageNumber', (pagination.pageIndex + 1).toString());
    params.append('pageSize', pagination.pageSize.toString());

    if (globalFilter) params.append('searchTerm', globalFilter);
    if (columnFilters.length > 0)
      params.append('columnFilters', JSON.stringify(columnFilters ?? []));
    if (sorting.length > 0)
      params.append('orderBy', JSON.stringify(sorting ?? []));

    return params;
  }

  useEffect(() => {
    if (columnFilters.length > 0) {
      setPagination((prevstate) => {
        return {
          ...prevstate,
          pageIndex: 0,
        };
      });
    }
    getData();
  }, [
    pagination.pageIndex,
    pagination.pageSize,
    globalFilter,
    columnFilters,
    sorting,
  ]);

  let source: any;
  const getData = async () => {
    if (!tableData.length) {
      setIsLoading(true);
    } else {
      setIsRefetching(true);
    }
    if (source) {
      source.cancel();
    }
    source = axios.CancelToken.source();
    const params = getAxiosParams(userParams);
    await httpAgent.SocialEvents.getSocialEventsList(params, source.token)
      .then((data) => {
        setRowCount(data.metaData.totalCount);

        setIsLoading(false);
        setIsRefetching(false);
      })
      .catch((error) => {
        if (!axios.isCancel(error)) {
          setIsLoading(false);
          setIsRefetching(false);
        }
      });
  };

  const columns = useMemo<MRT_ColumnDef<UsersModel>[]>(
    () => [
      {
        header: 'User Id',
        accessorKey: 'user_id',
      },
      {
        header: 'User Name',
        accessorKey: 'user_name',
      },
      {
        header: 'User Age',
        accessorKey: 'user_age',
      },
    ],

    []
  );

  return (
    <Fragment>
      <Paper>
        <Box>
          <MaterialReactTable
            columns={columns}
            data={tableData}
            //enableRowSelection
            //getRowId={(row) => row.customerName}
            initialState={{ showColumnFilters: false }}
            manualFiltering
            manualPagination
            manualSorting
            muiToolbarAlertBannerProps={
              isError
                ? {
                    color: 'error',
                    children: 'Error loading data',
                  }
                : undefined
            }
            onColumnFiltersChange={setColumnFilters}
            onGlobalFilterChange={setGlobalFilter}
            onPaginationChange={setPagination}
            onSortingChange={setSorting}
            rowCount={rowCount}
            state={{
              columnFilters,
              globalFilter,
              isLoading,
              pagination,
              showAlertBanner: isError,
              showProgressBars: isRefetching,
              sorting,
            }}
            muiTablePaperProps={{
              elevation: 0,
              sx: { borderRadius: '0', border: '1px solid #8d8c8c5e' },
            }}
            enableFullScreenToggle={false}
            enableDensityToggle={false}
            enableGrouping
            enableStickyHeader
            enableRowActions
            enableSorting={false}
            displayColumnDefOptions={{
              'mrt-row-actions': {
                header: '', //change header text
              },
            }}
          />
        </Box>
      </Paper>
    </Fragment>
  );
}
