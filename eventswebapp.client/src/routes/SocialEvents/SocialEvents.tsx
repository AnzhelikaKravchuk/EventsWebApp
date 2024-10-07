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
import {
  BottomNavigation,
  Button,
  Container,
  Drawer,
  Grid2,
  Pagination,
  Toolbar,
} from '@mui/material';
import { ChevronLeft, ChevronRight } from '@mui/icons-material';

const drawerWidth = 500;
const pageSize = 8;

const SocialEvents = () => {
  const { role } = useAuth();
  const [items, setItems] = useState<Array<SocialEventModel>>([]);
  const [filters, setFilters] = useState(new FormData());
  const [pageIndex, setPageIndex] = useState(1);
  const [totalPages, setTotalPages] = useState(0);
  const [itemsLoading, setItemsLoading] = useState(true);

  useEffect(() => {
    const fetchEvents = async () => {
      setItemsLoading(true);
      return await GetSocialEvents(filters, pageIndex, pageSize)
        .then((res) => {
          console.log('dermo', res.totalPages);
          setItems(res.items.$values);
          setTotalPages(res.totalPages);
        })
        .finally(() => {
          setItemsLoading(false);
        });
    };
    fetchEvents();
  }, [pageIndex, filters]);

  useEffect(() => console.log(totalPages), [totalPages]);

  const handlePageClick = (event: unknown, value: number) => {
    setPageIndex(value);
  };

  return (
    <Grid2
      container
      direction={'column'}
      justifyContent={'space-between'}
      alignItems={'center'}
      sx={{ height: '90vh' }}>
      <Container maxWidth='xl'>
        <NavLink hidden={role !== Role.Admin} to='/createEvent'>
          Create New Social Event
        </NavLink>

        {!itemsLoading ? (
          <Items currentItems={items} drawerWidth={drawerWidth} />
        ) : (
          'Loading...'
        )}
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
      </Container>
      <Pagination
        count={totalPages}
        onChange={handlePageClick}
        sx={{ mr: 50 }}
      />
    </Grid2>
  );
};

export default SocialEvents;
