import { observer } from 'mobx-react-lite';
import React, { useEffect, useState } from 'react';
import InfiniteScroll from 'react-infinite-scroller';
import { Grid, Loader } from 'semantic-ui-react';
import { PagingParams } from '../../../app/models/pagination';
import { useStore } from '../../../app/stores/store';
import HistoryFilters from './HistoryFilters';
import GameList from './GameList';

export default observer(function GameDashboard() {
    const { historyStore } = useStore();
    const { loadHistory, gameRegistry, setPagingParams, pagination } = historyStore;
    const [loadingNext, setLoadingNext] = useState(false);

    function handleGetNext() {
        setLoadingNext(true);
        setPagingParams(new PagingParams(pagination!.currentPage + 1))
        loadHistory().then(() => setLoadingNext(false));
    }

    useEffect(() => {
        if (gameRegistry.size <= 1) loadHistory();
    }, [gameRegistry.size, loadHistory])

    return (
        <Grid>
            <Grid.Column width='10'>
                        <InfiniteScroll
                            pageStart={0}
                            loadMore={handleGetNext}
                            hasMore={!loadingNext && !!pagination && pagination.currentPage < pagination.totalPages}
                            initialLoad={false}
                        >
                            <GameList />
                        </InfiniteScroll>
            </Grid.Column>
            <Grid.Column width='6'>
                <HistoryFilters />
            </Grid.Column>
            <Grid.Column width={10}>
                <Loader active={loadingNext} />
            </Grid.Column>
        </Grid>
    )
})