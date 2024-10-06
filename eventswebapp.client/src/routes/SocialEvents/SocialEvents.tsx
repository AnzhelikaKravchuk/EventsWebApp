import ReactPaginate from 'react-paginate';
import { Repository } from '../../utils/Repository';
import { useEffect, useState } from 'react';
import { SocialEventModel } from '../../types/types';
import EventPage from './EventPage';
import { NavLink } from 'react-router-dom';
import { GetSocialEvents } from '../../services/socialEvents';
import Items from '../../components/Items/Items';

const SocialEvents = () => {
  const [items, setItems] = useState<Array<SocialEventModel>>([]);
  const [pageIndex, setPageIndex] = useState(1);
  const [pageSize, setPageSize] = useState(5);
  const [totalPages, setTotalPages] = useState(0);
  const [itemsLoading, setItemsLoading] = useState(true);

  useEffect(() => {
    const fetchEvents = async () => {
      setItemsLoading(true);
      return await GetSocialEvents(pageIndex, pageSize)
        .then((res) => {
          setItems(res.items.$values);
          setTotalPages(res.totalPages);
        })
        .finally(() => {
          setItemsLoading(false);
        });
    };
    fetchEvents();
  }, [pageIndex, pageSize]);

  const handlePageClick = (event) => {
    setPageIndex(event.selected + 1);
  };

  return (
    <div>
      <NavLink to='/createEvent'>Create New Social Event</NavLink>
      {!itemsLoading ? <Items currentItems={items} /> : 'Loading...'}
      <ReactPaginate
        breakLabel='...'
        nextLabel='next >'
        onPageChange={handlePageClick}
        pageRangeDisplayed={pageSize}
        pageCount={totalPages}
        previousLabel='< previous'
        renderOnZeroPageCount={null}
      />
    </div>
  );
};

export default SocialEvents;
