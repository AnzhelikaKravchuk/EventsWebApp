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

  useEffect(() => {
    GetSocialEvents(pageIndex, pageSize).then((res) => {
      setItems(res.items.$values);
      setTotalPages(res.totalPages);
    });
  }, [pageIndex, pageSize]);

  const handlePageClick = (event) => {
    setPageIndex(event.selected + 1);
  };

  return (
    <div>
      <NavLink to='/createEvent'>Create New Social Event</NavLink>
      <Items currentItems={items} />
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
