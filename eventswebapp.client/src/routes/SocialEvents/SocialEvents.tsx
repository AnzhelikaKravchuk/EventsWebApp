import ReactPaginate from 'react-paginate';
import { Repository } from '../../utils/Repository';
import { useEffect, useState } from 'react';
import { SocialEventModel } from '../../types/types';
import EventPage from './EventPage';
import { NavLink } from 'react-router-dom';

interface Props {
  currentItems: Array<SocialEventModel>;
}
function Items(props: Props) {
  return (
    <>
      {props.currentItems &&
        props.currentItems.map((item) => (
          <NavLink to='/eventPage' state={item}>
            <p>{item.eventName}</p>
          </NavLink>
        ))}
    </>
  );
}

const SocialEvents = () => {
  const [items, setItems] = useState<Array<SocialEventModel>>([
    {
      id: '1',
      eventName: 'Name',
      description: 'Description',
      place: 'Minsk',
      date: new Date(),
      category: 'Other',
      maxAttendee: 1,
      attendees: [],
      image: '',
    },
  ]);
  const [pageIndex, setPageIndex] = useState(1);
  const [pageSize, setPageSize] = useState(2);
  const [totalPages, setTotalPages] = useState(0);

  useEffect(() => {
    Repository.GetSocialEvents(pageIndex, pageSize).then((res) => {
      setItems(res.currentItems);
      setTotalPages(res.pageCount);
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
