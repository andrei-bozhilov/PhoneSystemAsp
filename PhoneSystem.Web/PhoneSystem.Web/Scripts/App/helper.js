var helper = helper || {};

helper.changeLocationClientSide = function (quaryString, updatePanelId) {
    window.history.pushState({}, 'page 2', quaryString);
    $('form').get(0).setAttribute('action', window.location);

    if (updatePanelId !== undefined) {
        __doPostBack(updatePanelId, '');
    }
}