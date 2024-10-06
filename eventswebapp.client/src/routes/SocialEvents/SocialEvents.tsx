import ReactPaginate from 'react-paginate';
import { Repository } from '../../utils/Repository';
import { useEffect, useState } from 'react';
import { Role, SocialEventModel } from '../../types/types';
import EventPage from './EventPage';
import { NavLink } from 'react-router-dom';
import { GetSocialEvents } from '../../services/socialEvents';
import Items from '../../components/Items/Items';
import Filters from '../../components/Items/Filters';
import { useAuth } from '../../hooks/useAuth';
import { Drawer, Grid2, Toolbar } from '@mui/material';

const drawerWidth = 500;

const SocialEvents = () => {
  const { role } = useAuth();
  const [items, setItems] = useState<Array<SocialEventModel>>([]);
  const [filters, setFilters] = useState(new FormData());
  const [pageIndex, setPageIndex] = useState(1);
  const [pageSize, setPageSize] = useState(5);
  const [totalPages, setTotalPages] = useState(0);
  const [itemsLoading, setItemsLoading] = useState(true);

  useEffect(() => {
    const fetchEvents = async () => {
      setItemsLoading(true);
      return await GetSocialEvents(filters, pageIndex, pageSize)
        .then((res) => {
          setItems(res.items.$values);
          setTotalPages(res.totalPages);
        })
        .finally(() => {
          setItemsLoading(false);
        });
    };
    fetchEvents();
  }, [pageIndex, pageSize, filters]);

  useEffect(() => console.log(totalPages), [totalPages]);

  const handlePageClick = (event) => {
    setPageIndex(event.selected + 1);
  };

  return (
    <>
      <NavLink hidden={role !== Role.Admin} to='/createEvent'>
        Create New Social Event
      </NavLink>

      {!itemsLoading ? <Items currentItems={items} /> : 'Loading...'}
      <Drawer
        variant='permanent'
        anchor='right'
        sx={{
          width: drawerWidth,
          '& .MuiDrawer-paper': {
            width: drawerWidth,
            boxSizing: 'border-box',
          },
        }}>
        <Toolbar />
        <Filters
          pageSize={pageSize}
          setItems={setItems}
          setPages={setTotalPages}
          setFilters={setFilters}
          setPageIndex={setPageIndex}
        />
      </Drawer>
      <ReactPaginate
        breakLabel='...'
        nextLabel='next >'
        onPageChange={handlePageClick}
        pageRangeDisplayed={totalPages}
        pageCount={totalPages}
        previousLabel='< previous'
        renderOnZeroPageCount={null}
      />
    </>
  );
};

export default SocialEvents;
