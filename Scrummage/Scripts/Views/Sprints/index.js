function loadScrumTasksTableData(sprintId, sprintIsInactive, loggedUserId) {
    var source = $('#taskBox').html();
    var template = Handlebars.compile(source);
    var table = $('#scrumBoard');

    $.get('/api/ScrumTasks?sprintId=' + sprintId + '&onlyActive=true', function (data) {
        data.forEach(function (task) {
            var taskBox = $(template(task));

            if (task.taskType === 2)
                taskBox.removeClass('js-moveable');
            else
                taskBox.find('.js-done').hide();

        var elementPosition = $('.js-user-row[data-user-id=' + task.user.id + ']')
            .find('.scrum-task-col[data-type-id=' + task.taskType + ']');

        taskBox.appendTo(elementPosition);
    });
}).done(function () {
    if (!sprintIsInactive) {
        makeTaskBoxesDraggable(loggedUserId);
    }

    $('.js-taskbox').on('mousedown', '.js-task-details-button', function () {
        var taskId = $(this).parents('.js-taskbox').attr('data-task-id');

        $.get('/api/ScrumTasks/GetScrumTaskContent?id=' + taskId, function (task) {
            var message = task.content + '<hr />' +
                '<div><strong>Priority: ' + task.priority + '</strong></div>' +
                '<div><strong>Estimation: ' + task.estimation.value + '</strong></div>' +
                '<div><strong>Who: ' + task.user.name + ' ' + task.user.surname + '</strong></div>';

            if (task.took != null)
                message += '<div><strong>Took: ' + task.took.value + '</strong></div>';

            bootbox.alert({
                size: "medium",
                title: task.title,
                message: message
            });
        });
    });

    table.dataTable();
});

function makeTaskBoxesDraggable(loggedUserId) {
    $('.js-user-row[data-user-id="' + loggedUserId + '"]').find('.js-moveable')
        .draggable({
            revert: "invalid",
            start: function (event, ui) {
                ui.helper.css('z-index', 1);
                ui.helper.parents('.js-user-row')
                    .children('.scrum-task-col')
                    .droppable('enable');
            },
            stop: function (event, ui) {
                ui.helper.parents('.js-user-row')
                    .children('.scrum-task-col')
                    .droppable('disable');
            }
        }).addClass('move-cursor');
    }
}

function bindSprintSelectboxEvent() {
    $('#sprintsList').on('change', function () {
        location.href = '/Sprints/' + $(this).val();
    });
}

function bindUpdateScrumTaskEvent(tookOptions) {
    $('.scrum-task-col').droppable({
        classes: {
            "ui-droppable-hover": "ui-state-hover"
        },
        drop: function (event, ui) {
            var taskType = $(this).attr('data-type-id');
            var taskBox = ui.draggable;
            var scrumTaskId = taskBox.attr('data-task-id');

            if (taskType === taskBox.parents('.scrum-task-col').attr('data-type-id')) {
                taskBox.removeAttr('style');
                return;
            }

            if (taskType === '2') {
                bootbox.prompt({
                    title: "How much took this task? " +
                        "<div><strong>Note that this operation cannot be undone!</strong></div>",
                    inputType: 'select',
                    inputOptions: tookOptions,
                    callback: function (took) {
                        if (took == null) {
                            taskBox.removeAttr('style');
                            return;
                        } else
                            updateScrumTask(taskBox, scrumTaskId, taskType, took);
                    }
                });
            } else
                updateScrumTask(taskBox, scrumTaskId, taskType);

        }
    }).droppable('disable');
}

function updateScrumTask(taskBox, scrumTaskId, taskType, took) {
    $.ajax({
        url: '/api/scrumtasks/' + scrumTaskId,
        method: 'patch',
        data: { taskType: taskType, tookId: took }
    })
    .done(function (data) {
        switch (taskType) {
            case '0':
                moveTaskBox(taskBox, 0);
            break;
            case '2':
                moveTaskBox(taskBox, 2, data.took);
            break;
            case '1':
                moveTaskBox(taskBox, 1);
            break;
        }
        toastr.success('Task status changed successfully');
    })
    .fail(function () {
        toastr.error('Something went wrong');
    });
}   

function moveTaskBox(taskBox, destination, took) {
    taskBox.find('js-took').text(took);

    if (destination === 2) {
        taskBox.find('.js-done').show();
        taskBox.removeClass('move-cursor').draggable('disable');
    }

    taskBox
        .appendTo(taskBox.parents('tr.js-user-row').find('.scrum-task-col[data-type-id="' + destination + '"]'))
        .removeAttr('style');
}

