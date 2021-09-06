import { observer } from 'mobx-react-lite';
import React, { useEffect } from 'react';
import { useParams } from 'react-router-dom';
import { Grid } from 'semantic-ui-react';
import LoadingComponent from '../../../app/layout/LoadingComponent';
import { useStore } from '../../../app/stores/store';
import GameDetailedInfo from './GameDetailedInfo';
import GameDetailedSidebar from './GameDetailedSidebar';
import GameDetailedHeader from './GameDetaledHeader';

export default observer(function GameDetails() {
    // const {historyStore} = useStore();
    // const {selectedGame: game, loadGame, loadingInitial, clearSelectedGame} = historyStore;
    // const {id} = useParams<{id: string}>();
    //
    // useEffect(() => {
    //     if (id) loadGame(id);
    //     return () => clearSelectedGame();
    // }, [id, loadGame, clearSelectedGame]);
    //
    // if (loadingInitial || !game) return <LoadingComponent />;

    return (
        <Grid>
            {/*<Grid.Column width={10}>*/}
            {/*    <GameDetailedHeader game={game} />*/}
            {/*    <GameDetailedInfo game={game} />*/}
            {/*</Grid.Column>*/}
            {/*<Grid.Column width={6}>*/}
            {/*    <GameDetailedSidebar game={game} />*/}
            {/*</Grid.Column>*/}
        </Grid>
    )
})