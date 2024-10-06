import ReactPaginate from 'react-paginate';
import { Repository } from '../../utils/Repository';
import { useEffect, useState } from 'react';
import { SocialEventModel } from '../../types/types';
import EventPage from './EventPage';
import { NavLink } from 'react-router-dom';
import { GetSocialEvents } from '../../services/socialEvents';
import Items from '../../components/Items/Items';
import Filters from '../../components/Items/Filters';

const SocialEvents = () => {
  const [items, setItems] = useState<Array<SocialEventModel>>([]);
  const [filters, setFilters] = useState(new FormData());
  const [pageIndex, setPageIndex] = useState(1);
  const [pageSize, setPageSize] = useState(2);
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
    <div>
      <NavLink to='/createEvent'>Create New Social Event</NavLink>
      {!itemsLoading ? <Items currentItems={items} /> : 'Loading...'}
      <Filters
        pageSize={pageSize}
        setItems={setItems}
        setPages={setTotalPages}
        setFilters={setFilters}
        setPageIndex={setPageIndex}
      />
      <ReactPaginate
        breakLabel='...'
        nextLabel='next >'
        onPageChange={handlePageClick}
        pageRangeDisplayed={totalPages}
        pageCount={totalPages}
        previousLabel='< previous'
        renderOnZeroPageCount={null}
      />
    </div>
  );
};

export default SocialEvents;
